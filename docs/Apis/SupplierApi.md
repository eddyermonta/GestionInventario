# SupplierApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addSupplier**](SupplierApi.md#addSupplier) | **POST** /api/supplier | Adds a supplier |
| [**deleteSupplier**](SupplierApi.md#deleteSupplier) | **DELETE** /api/supplier/{NIT} | Deletes a supplier |
| [**getAllSuppliers**](SupplierApi.md#getAllSuppliers) | **GET** /api/supplier | Gets all suppliers |
| [**getSupplierByNIT**](SupplierApi.md#getSupplierByNIT) | **GET** /api/supplier/{NIT} | Gets a supplier by its NIT |
| [**updateSupplier**](SupplierApi.md#updateSupplier) | **PUT** /api/supplier/{NIT} | Updates a supplier |


<a name="addSupplier"></a>
# **addSupplier**
> SupplierDto addSupplier(SupplierDto)

Adds a supplier

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **SupplierDto** | [**SupplierDto**](../Models/SupplierDto.md)| Supplier to add | [optional] |

### Return type

[**SupplierDto**](../Models/SupplierDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="deleteSupplier"></a>
# **deleteSupplier**
> deleteSupplier(NIT)

Deletes a supplier

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**| The NIT of the supplier to delete. | [default to null] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getAllSuppliers"></a>
# **getAllSuppliers**
> List getAllSuppliers()

Gets all suppliers

### Parameters
This endpoint does not need any parameter.

### Return type

[**List**](../Models/SupplierDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getSupplierByNIT"></a>
# **getSupplierByNIT**
> SupplierResponse getSupplierByNIT(NIT)

Gets a supplier by its NIT

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**| The NIT of the supplier to search for. | [default to null] |

### Return type

[**SupplierResponse**](../Models/SupplierResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateSupplier"></a>
# **updateSupplier**
> updateSupplier(NIT, SupplierUpdateDto)

Updates a supplier

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**| The NIT of the supplier to update. | [default to null] |
| **SupplierUpdateDto** | [**SupplierUpdateDto**](../Models/SupplierUpdateDto.md)| Supplier to update | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

