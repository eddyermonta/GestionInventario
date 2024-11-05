# ProductApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addProduct**](ProductApi.md#addProduct) | **POST** /api/product/{NIT} | Adds a product to a supplier and assigns categories to the product and her movement |
| [**deleteProduct**](ProductApi.md#deleteProduct) | **DELETE** /api/product/{name} | deletes a product |
| [**updateProduct**](ProductApi.md#updateProduct) | **PUT** /api/product/{name} | Updates a product |


<a name="addProduct"></a>
# **addProduct**
> ProductResponse addProduct(NIT, ProductRequest)

Adds a product to a supplier and assigns categories to the product and her movement

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**| NIT of the supplier to which the product will be added | [default to null] |
| **ProductRequest** | [**ProductRequest**](../Models/ProductRequest.md)| Object containing the information of the product to be added | [optional] |

### Return type

[**ProductResponse**](../Models/ProductResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="deleteProduct"></a>
# **deleteProduct**
> deleteProduct(name)

deletes a product

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**| Name of the product to be deleted | [default to null] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateProduct"></a>
# **updateProduct**
> updateProduct(name, ProductResponse)

Updates a product

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**| Name of the product to be updated | [default to null] |
| **ProductResponse** | [**ProductResponse**](../Models/ProductResponse.md)| Object containing the information of the product to be updated | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

