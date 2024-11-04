# InventoryApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiInventoryUpdatebysupplierreceiptPost**](InventoryApi.md#apiInventoryUpdatebysupplierreceiptPost) | **POST** /api/inventory/updatebysupplierreceipt | Actualiza el inventario basado en un recibo de proveedor cargado desde un archivo Excel. |
| [**getAllProducts**](InventoryApi.md#getAllProducts) | **GET** /api/inventory |  |
| [**getProductByName**](InventoryApi.md#getProductByName) | **GET** /api/inventory/product/{name} |  |
| [**getProductsByCategoryName**](InventoryApi.md#getProductsByCategoryName) | **GET** /api/inventory/{categoryName} |  |
| [**updateInventory**](InventoryApi.md#updateInventory) | **PUT** /api/inventory/{movementType} |  |


<a name="apiInventoryUpdatebysupplierreceiptPost"></a>
# **apiInventoryUpdatebysupplierreceiptPost**
> apiInventoryUpdatebysupplierreceiptPost(file)

Actualiza el inventario basado en un recibo de proveedor cargado desde un archivo Excel.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **file** | **File**| El archivo Excel que contiene el recibo del proveedor. | [optional] [default to null] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: multipart/form-data
- **Accept**: text/plain, application/json, text/json

<a name="getAllProducts"></a>
# **getAllProducts**
> List getAllProducts()



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



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**|  | [default to null] |

### Return type

[**ProductResponse**](../Models/ProductResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getProductsByCategoryName"></a>
# **getProductsByCategoryName**
> CategoryProductsDto getProductsByCategoryName(categoryName)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **categoryName** | **String**|  | [default to null] |

### Return type

[**CategoryProductsDto**](../Models/CategoryProductsDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateInventory"></a>
# **updateInventory**
> String updateInventory(movementType, movementCategory, MovementRequest)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **movementType** | [**MovementType**](../Models/.md)| El tipo de movimiento (Manual o Automatic). | [default to null] [enum: 0, 1] |
| **movementCategory** | [**MovementCategory**](../Models/.md)| La categoría del movimiento (Entrada o Salida). | [default to null] [enum: 0, 1] |
| **MovementRequest** | [**MovementRequest**](../Models/MovementRequest.md)| La solicitud de movimiento que contiene los detalles del movimiento. | [optional] |

### Return type

**String**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

