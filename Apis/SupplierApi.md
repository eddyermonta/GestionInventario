# SupplierApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addSupplier**](SupplierApi.md#addSupplier) | **POST** /api/supplier |  |
| [**deleteSupplier**](SupplierApi.md#deleteSupplier) | **DELETE** /api/supplier/{NIT} |  |
| [**getAllSuppliers**](SupplierApi.md#getAllSuppliers) | **GET** /api/supplier |  |
| [**getSupplierByNIT**](SupplierApi.md#getSupplierByNIT) | **GET** /api/supplier/{NIT} |  |
| [**updateSupplier**](SupplierApi.md#updateSupplier) | **PUT** /api/supplier/{NIT} |  |


<a name="addSupplier"></a>
# **addSupplier**
> SupplierDto addSupplier(SupplierDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **SupplierDto** | [**SupplierDto**](../Models/SupplierDto.md)|  | [optional] |

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



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**|  | [default to null] |

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
> SupplierDto getSupplierByNIT(NIT)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**|  | [default to null] |

### Return type

[**SupplierDto**](../Models/SupplierDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateSupplier"></a>
# **updateSupplier**
> updateSupplier(NIT, SupplierUpdateDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **NIT** | **String**|  | [default to null] |
| **SupplierUpdateDto** | [**SupplierUpdateDto**](../Models/SupplierUpdateDto.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

