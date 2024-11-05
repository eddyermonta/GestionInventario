# InventoryApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**getAllProducts**](InventoryApi.md#getAllProducts) | **GET** /api/inventory | Gets all products. |
| [**getProductByName**](InventoryApi.md#getProductByName) | **GET** /api/inventory/product/{name} | Gets a product by its name. |
| [**getProductsByCategoryName**](InventoryApi.md#getProductsByCategoryName) | **GET** /api/inventory/{categoryName} | Gets the products of a category. |
| [**updateBySupplierReceipt**](InventoryApi.md#updateBySupplierReceipt) | **PUT** /api/inventory/supplierreceipt | Updates the inventory of products by supplier receipt. |
| [**updateInventory**](InventoryApi.md#updateInventory) | **PUT** /api/inventory/{movementCategory} | Updates the inventory of products. |


<a name="getAllProducts"></a>
# **getAllProducts**
> List getAllProducts()

Gets all products.

### Parameters
This endpoint does not need any parameter.

### Return type

[**List**](../Models/ProductResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getProductByName"></a>
# **getProductByName**
> ProductResponse getProductByName(name)

Gets a product by its name.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**| The name of the product to search for. | [default to null] |

### Return type

[**ProductResponse**](../Models/ProductResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getProductsByCategoryName"></a>
# **getProductsByCategoryName**
> CategoryProductsResponse getProductsByCategoryName(categoryName)

Gets the products of a category.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **categoryName** | **String**| The name of the category. | [default to null] |

### Return type

[**CategoryProductsResponse**](../Models/CategoryProductsResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateBySupplierReceipt"></a>
# **updateBySupplierReceipt**
> String updateBySupplierReceipt(supplierReceipt)

Updates the inventory of products by supplier receipt.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **supplierReceipt** | **File**| The Excel file with the supplier receipt information. | [optional] [default to null] |

### Return type

**String**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: multipart/form-data
- **Accept**: text/plain, application/json, text/json

<a name="updateInventory"></a>
# **updateInventory**
> MovementResponse updateInventory(movementCategory, MovementRequest)

Updates the inventory of products.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **movementCategory** | [**MovementCategory**](../Models/.md)| The category of the movement (entry or exit). | [default to null] [enum: 0, 1] |
| **MovementRequest** | [**MovementRequest**](../Models/MovementRequest.md)| The information of the movement. | [optional] |

### Return type

[**MovementResponse**](../Models/MovementResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

