# CategoryApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addCategory**](CategoryApi.md#addCategory) | **POST** /api/category |  |
| [**deleteCategory**](CategoryApi.md#deleteCategory) | **DELETE** /api/category |  |
| [**getAllCategories**](CategoryApi.md#getAllCategories) | **GET** /api/category |  |
| [**getCategoryByName**](CategoryApi.md#getCategoryByName) | **GET** /api/category/{name} |  |
| [**updateCategory**](CategoryApi.md#updateCategory) | **PUT** /api/category |  |


<a name="addCategory"></a>
# **addCategory**
> CategoryDto addCategory(CategoryDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **CategoryDto** | [**CategoryDto**](../Models/CategoryDto.md)|  | [optional] |

### Return type

[**CategoryDto**](../Models/CategoryDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="deleteCategory"></a>
# **deleteCategory**
> deleteCategory(CategoryDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **CategoryDto** | [**CategoryDto**](../Models/CategoryDto.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="getAllCategories"></a>
# **getAllCategories**
> List getAllCategories()



### Parameters
This endpoint does not need any parameter.

### Return type

[**List**](../Models/CategoryDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getCategoryByName"></a>
# **getCategoryByName**
> CategoryDto getCategoryByName(name)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**|  | [default to null] |

### Return type

[**CategoryDto**](../Models/CategoryDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateCategory"></a>
# **updateCategory**
> updateCategory(CategoryDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **CategoryDto** | [**CategoryDto**](../Models/CategoryDto.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

