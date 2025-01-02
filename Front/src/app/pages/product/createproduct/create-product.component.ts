import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms'; 
import { lastValueFrom } from 'rxjs';


import { MessageService, SharedModule } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';;
import { ProductService } from '../product.service';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-create-product',
  standalone: true,
  imports: [SharedModule,DialogModule,InputTextModule,FormsModule,CommonModule,ToastModule],
  templateUrl: './create-product.component.html',
  styleUrl: './create-product.component.css',
  providers:[MessageService]
})
export class CreateproductComponent {
  //Decorators for send response to parent and getting value from parent
  @Input() productId: number | null = null;
  @Output() productCreated = new EventEmitter<{ success: boolean, message: string }>();

  visible: boolean = true;
  isLoading : boolean = false;
  showValidationMessage = false;
  validationMessage : string;

  product = {
    name: '',
    description: '',
    price: 0.00
  };

 constructor(private productService : ProductService,private messageService : MessageService){}

 ngOnChanges(changes: SimpleChanges) {
  if (changes['productId'] && this.productId != null) {
  this.getProduct(this.productId)
  }
}

  onSubmit(form: NgForm){
    this.isLoading = true;
    if(!form.valid){
      this.showValidationMessage = true;
      this.validationMessage = " Please Fill All Required Fields."
      this.isLoading = false;
      return;
    }
   
    if(!this.isProductValid()){
      this.showValidationMessage = true;
      this.validationMessage = this.product.price <= 0 ? 'Price must be greater than zero.' : 'Please fill all required fields.';
      this.isLoading = false;
      return;
    }  
    this.showValidationMessage = false;
    if(this.productId == null){
      lastValueFrom(this.productService.createProduct(this.product)).then(response => {
        if(response.isSuccess){
          this.productCreated.emit({ success: response.isSuccess, message: response.message });
          this.isLoading = false;
          this.resetForm();
        }else{
          this.isLoading = false;
          this.messageService.add({ severity: 'Product Creation', summary: 'Error', detail: response.message });
        }
      })
      .catch(error => {
        this.isLoading = false;  // Hiding loader on error
        this.productCreated.emit({ success: error.isSuccess, message: error.message });
      });
    }
    else{
     lastValueFrom(this.productService.updateProduct(this.productId,this.product)).then(response => {
      if(response.isSuccess){
        this.productCreated.emit({ success: response.isSuccess, message: response.message });
        this.isLoading = false;
        this.resetForm();
      }else{
        this.isLoading = false;
        this.messageService.add({ severity: 'Product Creation', summary: 'Error', detail: response.message });
      }
     }).catch(error => {
      this.isLoading = false;  // Hiding loader on error
      this.productCreated.emit({ success: error.isSuccess, message: error.message });
    });
    }
  }

  getProduct(id : number){
    lastValueFrom(this.productService.getById(id)).then((response : any) => {
      this.product = response.result
    })
  }

  isProductValid(): boolean {
    return this.product.name !== null && this.product.name !== '' && 
    this.product.description !== null && this.product.description !== '' && 
    this.product.price !== null && this.product.price > 0;
  }

  resetForm() {
    this.product = { name: '', description: '', price: 0.00 };
  }
}
