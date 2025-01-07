import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../product.service';
import { lastValueFrom } from 'rxjs';
import { Product } from '../model/product';
import { Table, TableModule } from 'primeng/table';
import { CommonModule, CurrencyPipe, DatePipe, NgIf } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ConfirmationService, MessageService, SharedModule } from 'primeng/api';
import { ToolbarModule } from 'primeng/toolbar';
import { DialogModule } from 'primeng/dialog';
import { CreateproductComponent } from '../createproduct/create-product.component';
import { ToastModule } from 'primeng/toast';
import { InputTextModule } from 'primeng/inputtext';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmDialogModule } from 'primeng/confirmdialog';


@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [InputTextModule,TableModule, CurrencyPipe, NgIf,DatePipe, ButtonModule, ToastModule,ToolbarModule, SharedModule, DialogModule, CreateproductComponent,InputTextModule,IconFieldModule, InputIconModule,TableModule, CommonModule,FormsModule,ReactiveFormsModule,ConfirmDialogModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
  providers: [ProductService, MessageService,ConfirmationService]
})
export class ProductlistComponent implements OnInit {

  selectedProductId : number;
  visible: boolean = false;
  filteredProducts: Product[] = [];
  searchText: string = "";
  isLoading : boolean = false;
  products: Product[];
  @ViewChild('dt') dt?: Table;


  constructor(private productService : ProductService,private messageService : MessageService, private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.searchText = "";
    this.getProducts();
  }
  getProducts() {
    this.isLoading = true;
    lastValueFrom(this.productService.getAllProducts()).then((response) => {
      this.products = response.result;
        this.filteredProducts = [...this.products];
      this.isLoading = false;
    }).catch((error) => {
      this.isLoading = false;
    });
  }
  openDialog() {
    this.visible = true;
  }
  closeDialog() {
    this.visible = false;
  }
  editProduct(event : Product){
    this.selectedProductId = event.id
    this.visible = true;    
  }

  handleProductCreated(event:any) {
    if (event.success) {
      this.closeDialog();  
      this.messageService.add({ severity: 'success', summary: 'Product Creation', detail: event.message }); 
      this.getProducts();
    } else {
      this.messageService.add({ severity: 'Product Creation', summary: 'Error', detail: event.message });
    }
  }


  filterProducts() {
    if (this.searchText) {
      this.filteredProducts = this.products.filter(product =>
        product.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
        product.price.toString().includes(this.searchText)
      );
    } else {
      this.filteredProducts = [...this.products]; // Reset to all products if search is empty
    }
  }
  confirmDelete(product: any) {
    console.log(product);
    
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this product?',
      header: 'Confirm Delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteProduct(product.id);
      },
      reject: () => {
        console.log('Delete cancelled');
      }
    });
  }
  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe(
      response => {
        this.messageService.add({ severity: 'success', summary: 'Product Deletion', detail: response.message }); 
        //this.products = this.products.filter(p => p.id !== id);
        this.getProducts();
      },
      error => {
        this.messageService.add({ severity: 'Product Creation', summary: 'Error', detail: error.message });
      }
    );
  }
}