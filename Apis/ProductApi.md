# ProductApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addProduct**](ProductApi.md#addProduct) | **POST** /api/product/{NIT} |  |
| [**deleteProduct**](ProductApi.md#deleteProduct) | **DELETE** /api/product/{name} |  |
| [**getAllProducts**](ProductApi.md#getAllProducts) | **GET** /api/product |  |
| [**getProductByName**](ProductApi.md#getProductByName) | **GET** /api/product/{name} |  |
| [**updateProduct**](ProductApi.md#updateProduct) | **PUT** /api/product/{name} |  |


<a name="addProduct"></a>
# **addProduct**
> ProductResponse addProduct(NIT, ProductRequest)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**|  | [default to null] |
| **ProductRequest** | [**ProductRequest**](../Models/ProductRequest.md)|  | [optional] |

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



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**|  | [default to null] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
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

<a name="updateProduct"></a>
# **updateProduct**
> updateProduct(name, ProductUpdateDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**|  | [default to null] |
| **ProductUpdateDto** | [**ProductUpdateDto**](../Models/ProductUpdateDto.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

