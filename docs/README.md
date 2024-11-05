# Documentation for My API

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *http://localhost*

| Class | Method | HTTP request | Description |
|------------ | ------------- | ------------- | -------------|
| *AuthApi* | [**apiAuthValidatePost**](Apis/AuthApi.md#apiauthvalidatepost) | **POST** /api/auth/validate | Validates if the user exists in the database. |
| *CategoryApi* | [**addCategories**](Apis/CategoryApi.md#addcategories) | **POST** /api/category | Adds a list of categories |
*CategoryApi* | [**deleteCategory**](Apis/CategoryApi.md#deletecategory) | **DELETE** /api/category/{name} | Elimina una categoría |
*CategoryApi* | [**getAllCategories**](Apis/CategoryApi.md#getallcategories) | **GET** /api/category | Gets all categories |
*CategoryApi* | [**getCategoryByName**](Apis/CategoryApi.md#getcategorybyname) | **GET** /api/category/{nameCategory} | Gets a category by its name |
*CategoryApi* | [**updateCategory**](Apis/CategoryApi.md#updatecategory) | **PUT** /api/category/{name} | Updates a category |
| *InventoryApi* | [**getAllProducts**](Apis/InventoryApi.md#getallproducts) | **GET** /api/inventory | Gets all products. |
*InventoryApi* | [**getProductByName**](Apis/InventoryApi.md#getproductbyname) | **GET** /api/inventory/product/{name} | Gets a product by its name. |
*InventoryApi* | [**getProductsByCategoryName**](Apis/InventoryApi.md#getproductsbycategoryname) | **GET** /api/inventory/{categoryName} | Gets the products of a category. |
*InventoryApi* | [**updateBySupplierReceipt**](Apis/InventoryApi.md#updatebysupplierreceipt) | **PUT** /api/inventory/supplierreceipt | Updates the inventory of products by supplier receipt. |
*InventoryApi* | [**updateInventory**](Apis/InventoryApi.md#updateinventory) | **PUT** /api/inventory/{movementCategory} | Updates the inventory of products. |
| *ProductApi* | [**addProduct**](Apis/ProductApi.md#addproduct) | **POST** /api/product/{NIT} | Adds a product to a supplier and assigns categories to the product and her movement |
*ProductApi* | [**deleteProduct**](Apis/ProductApi.md#deleteproduct) | **DELETE** /api/product/{name} | deletes a product |
*ProductApi* | [**updateProduct**](Apis/ProductApi.md#updateproduct) | **PUT** /api/product/{name} | Updates a product |
| *SupplierApi* | [**addSupplier**](Apis/SupplierApi.md#addsupplier) | **POST** /api/supplier | Adds a supplier |
*SupplierApi* | [**deleteSupplier**](Apis/SupplierApi.md#deletesupplier) | **DELETE** /api/supplier/{NIT} | Deletes a supplier |
*SupplierApi* | [**getAllSuppliers**](Apis/SupplierApi.md#getallsuppliers) | **GET** /api/supplier | Gets all suppliers |
*SupplierApi* | [**getSupplierByNIT**](Apis/SupplierApi.md#getsupplierbynit) | **GET** /api/supplier/{NIT} | Gets a supplier by its NIT |
*SupplierApi* | [**updateSupplier**](Apis/SupplierApi.md#updatesupplier) | **PUT** /api/supplier/{NIT} | Updates a supplier |
| *UserApi* | [**addUser**](Apis/UserApi.md#adduser) | **POST** /api/user | Adds a new user. |
*UserApi* | [**getAllUsers**](Apis/UserApi.md#getallusers) | **GET** /api/user | Retrieves all users. |
*UserApi* | [**getUserByEmail**](Apis/UserApi.md#getuserbyemail) | **GET** /api/user/{email} | Retrieves a user by their email address. |
*UserApi* | [**updateUser**](Apis/UserApi.md#updateuser) | **PUT** /api/user/{email} | update a user by their email address. |


<a name="documentation-for-models"></a>
## Documentation for Models

 - [AddressRequest](./Models/AddressRequest.md)
 - [AddressResponse](./Models/AddressResponse.md)
 - [AddressUpdateRequest](./Models/AddressUpdateRequest.md)
 - [AuthRequest](./Models/AuthRequest.md)
 - [AuthResponse](./Models/AuthResponse.md)
 - [CategoryProductsResponse](./Models/CategoryProductsResponse.md)
 - [CategoryRequest](./Models/CategoryRequest.md)
 - [CategoryResponseName](./Models/CategoryResponseName.md)
 - [DocumentType](./Models/DocumentType.md)
 - [Mesurement](./Models/Mesurement.md)
 - [MovementCategory](./Models/MovementCategory.md)
 - [MovementRequest](./Models/MovementRequest.md)
 - [MovementResponse](./Models/MovementResponse.md)
 - [ProblemDetails](./Models/ProblemDetails.md)
 - [ProductRequest](./Models/ProductRequest.md)
 - [ProductResponse](./Models/ProductResponse.md)
 - [SupplierDto](./Models/SupplierDto.md)
 - [SupplierResponse](./Models/SupplierResponse.md)
 - [SupplierUpdateDto](./Models/SupplierUpdateDto.md)
 - [UnitMeasurement](./Models/UnitMeasurement.md)
 - [UserRequest](./Models/UserRequest.md)
 - [UserResponse](./Models/UserResponse.md)
 - [UserUpdateRequest](./Models/UserUpdateRequest.md)


<a name="documentation-for-authorization"></a>
## Documentation for Authorization

All endpoints do not require authorization.
