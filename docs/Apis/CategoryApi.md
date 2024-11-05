# CategoryApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addCategories**](CategoryApi.md#addCategories) | **POST** /api/category | Adds a list of categories |
| [**deleteCategory**](CategoryApi.md#deleteCategory) | **DELETE** /api/category/{name} | Elimina una categoría |
| [**getAllCategories**](CategoryApi.md#getAllCategories) | **GET** /api/category | Gets all categories |
| [**getCategoryByName**](CategoryApi.md#getCategoryByName) | **GET** /api/category/{nameCategory} | Gets a category by its name |
| [**updateCategory**](CategoryApi.md#updateCategory) | **PUT** /api/category/{name} | Updates a category |


<a name="addCategories"></a>
# **addCategories**
> CategoryResponseName addCategories(CategoryRequest)

Adds a list of categories

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **CategoryRequest** | [**CategoryRequest**](../Models/CategoryRequest.md)| List of categories | [optional] |

### Return type

[**CategoryResponseName**](../Models/CategoryResponseName.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="deleteCategory"></a>
# **deleteCategory**
> deleteCategory(name)

Elimina una categoría

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

<a name="getAllCategories"></a>
# **getAllCategories**
> List getAllCategories()

Gets all categories

### Parameters
This endpoint does not need any parameter.

### Return type

[**List**](../Models/CategoryResponseName.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getCategoryByName"></a>
# **getCategoryByName**
> CategoryResponseName getCategoryByName(nameCategory)

Gets a category by its name

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **nameCategory** | **String**| Name of the category | [default to null] |

### Return type

[**CategoryResponseName**](../Models/CategoryResponseName.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateCategory"></a>
# **updateCategory**
> updateCategory(name, CategoryResponseName)

Updates a category

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **name** | **String**| Name of the category | [default to null] |
| **CategoryResponseName** | [**CategoryResponseName**](../Models/CategoryResponseName.md)| Category to update | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

