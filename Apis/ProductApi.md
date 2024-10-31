# ProductApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addProduct**](ProductApi.md#addProduct) | **POST** /api/product |  |
| [**deleteProduct**](ProductApi.md#deleteProduct) | **DELETE** /api/product |  |
| [**getAllProducts**](ProductApi.md#getAllProducts) | **GET** /api/product |  |
| [**getProductByName**](ProductApi.md#getProductByName) | **GET** /api/product/{name} |  |
| [**updateProduct**](ProductApi.md#updateProduct) | **PUT** /api/product |  |


<a name="addProduct"></a>
# **addProduct**
> ProductDto addProduct(ProductDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **ProductDto** | [**ProductDto**](../Models/ProductDto.md)|  | [optional] |

### Return type

[**ProductDto**](../Models/ProductDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="deleteProduct"></a>
# **deleteProduct**
> deleteProduct(ProductDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **ProductDto** | [**ProductDto**](../Models/ProductDto.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="getAllProducts"></a>
# **getAllProducts**
> List getAllProducts()



### Parameters
This endpoint does not need any parameter.

### Return type

[**List**](../Models/ProductDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getProductByName"></a>
# **getProductByName**
> ProductDto getProductByName(name)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**|  | [default to null] |

### Return type

[**ProductDto**](../Models/ProductDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateProduct"></a>
# **updateProduct**
> updateProduct(ProductDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **ProductDto** | [**ProductDto**](../Models/ProductDto.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

