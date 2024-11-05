# Documentation for My API

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *http://localhost*

| Class | Method | HTTP request | Description |
|------------ | ------------- | ------------- | -------------|
| *AuthApi* | [**apiAuthValidatePost**](Apis/AuthApi.md#apiauthvalidatepost) | **POST** /api/auth/validate |  |
| *CategoryApi* | [**addCategories**](Apis/CategoryApi.md#addcategories) | **POST** /api/category |  |
*CategoryApi* | [**deleteCategory**](Apis/CategoryApi.md#deletecategory) | **DELETE** /api/category/{name} |  |
*CategoryApi* | [**getAllCategories**](Apis/CategoryApi.md#getallcategories) | **GET** /api/category |  |
*CategoryApi* | [**getCategoryByName**](Apis/CategoryApi.md#getcategorybyname) | **GET** /api/category/{nameCategory} |  |
*CategoryApi* | [**updateCategory**](Apis/CategoryApi.md#updatecategory) | **PUT** /api/category/{name} |  |
| *InventoryApi* | [**apiInventoryUpdatebysupplierreceiptPost**](Apis/InventoryApi.md#apiinventoryupdatebysupplierreceiptpost) | **POST** /api/inventory/updatebysupplierreceipt | Actualiza el inventario basado en un recibo de proveedor cargado desde un archivo Excel. |
*InventoryApi* | [**getAllProducts**](Apis/InventoryApi.md#getallproducts) | **GET** /api/inventory |  |
*InventoryApi* | [**getProductByName**](Apis/InventoryApi.md#getproductbyname) | **GET** /api/inventory/product/{name} |  |
*InventoryApi* | [**getProductsByCategoryName**](Apis/InventoryApi.md#getproductsbycategoryname) | **GET** /api/inventory/{categoryName} |  |
*InventoryApi* | [**updateInventory**](Apis/InventoryApi.md#updateinventory) | **PUT** /api/inventory/{movementType} |  |
| *ProductApi* | [**addProduct**](Apis/ProductApi.md#addproduct) | **POST** /api/product/{NIT} |  |
*ProductApi* | [**deleteProduct**](Apis/ProductApi.md#deleteproduct) | **DELETE** /api/product/{name} |  |
*ProductApi* | [**updateProduct**](Apis/ProductApi.md#updateproduct) | **PUT** /api/product/{name} |  |
| *SupplierApi* | [**addSupplier**](Apis/SupplierApi.md#addsupplier) | **POST** /api/supplier |  |
*SupplierApi* | [**deleteSupplier**](Apis/SupplierApi.md#deletesupplier) | **DELETE** /api/supplier/{NIT} |  |
*SupplierApi* | [**getAllSuppliers**](Apis/SupplierApi.md#getallsuppliers) | **GET** /api/supplier |  |
*SupplierApi* | [**getSupplierByNIT**](Apis/SupplierApi.md#getsupplierbynit) | **GET** /api/supplier/{NIT} |  |
*SupplierApi* | [**updateSupplier**](Apis/SupplierApi.md#updatesupplier) | **PUT** /api/supplier/{NIT} |  |
| *UserApi* | [**addUser**](Apis/UserApi.md#adduser) | **POST** /api/user |  |
*UserApi* | [**getAllUsers**](Apis/UserApi.md#getallusers) | **GET** /api/user |  |
*UserApi* | [**getUserByEmail**](Apis/UserApi.md#getuserbyemail) | **GET** /api/user/{email} |  |
*UserApi* | [**updateUser**](Apis/UserApi.md#updateuser) | **PUT** /api/user/{email} |  |


<a name="documentation-for-models"></a>
## Documentation for Models

 - [AddressDto](./Models/AddressDto.md)
 - [AddressGetElementDto](./Models/AddressGetElementDto.md)
 - [AddressUpdateDto](./Models/AddressUpdateDto.md)
 - [AuthRequest](./Models/AuthRequest.md)
 - [AuthResponse](./Models/AuthResponse.md)
 - [CategoryDto](./Models/CategoryDto.md)
 - [CategoryProductsDto](./Models/CategoryProductsDto.md)
 - [CategoryRequest](./Models/CategoryRequest.md)
 - [DocumentType](./Models/DocumentType.md)
 - [Mesurement](./Models/Mesurement.md)
 - [MovementCategory](./Models/MovementCategory.md)
 - [MovementRequest](./Models/MovementRequest.md)
 - [MovementType](./Models/MovementType.md)
 - [ProblemDetails](./Models/ProblemDetails.md)
 - [ProductRequest](./Models/ProductRequest.md)
 - [ProductResponse](./Models/ProductResponse.md)
 - [ProductUpdateDto](./Models/ProductUpdateDto.md)
 - [SupplierDto](./Models/SupplierDto.md)
 - [SupplierGetElementDto](./Models/SupplierGetElementDto.md)
 - [SupplierUpdateDto](./Models/SupplierUpdateDto.md)
 - [UnitMeasurement](./Models/UnitMeasurement.md)
 - [UserDto](./Models/UserDto.md)
 - [UserUpdateDto](./Models/UserUpdateDto.md)


<a name="documentation-for-authorization"></a>
## Documentation for Authorization

All endpoints do not require authorization.