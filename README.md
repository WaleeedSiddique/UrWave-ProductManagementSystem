Documentation For Api And UI Project
**WHAT I HAVE DONE**
**BACKEND**
1 - On the backend i have created minimal apis for product crud and grouped it into one class file.
2 - In the Contract folder you will find Dto's used to transport data from api to the frontend.
3 - I have used a generic response type so the response would be deliverd properly in an object.

**Product Compilation And Database Creation**
After cloning the repo and opening backend project please first build the project so that all the packages can restore as i have pushed the migration file also so you will need to just create the database which will be created by command
**UPDATE-DATABASE**
in case if the commands fails so your microsoft.entityframeworkcore.tools package might not have restored properly so you will have to install it again.

**FRONTEND**
1 - On Frontend i have mainly worked in two components CREATE-PRODUCT and LIST-PRODUCTS.
2 - I have used create product component for both create and update purpose.

**Product Compilation**
To run frontend project you first need to install the package by the command **npm install** as the project version is 18 so you will need node version greater than 18.19.0 after running this command serve the project with command 
**ng serve** please get the URL on which yoour backend project is running and put it in the product service as base url before /api/
