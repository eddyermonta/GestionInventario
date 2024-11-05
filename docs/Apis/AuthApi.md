# AuthApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiAuthValidatePost**](AuthApi.md#apiAuthValidatePost) | **POST** /api/auth/validate | Validates if the user exists in the database. |


<a name="apiAuthValidatePost"></a>
# **apiAuthValidatePost**
> AuthResponse apiAuthValidatePost(AuthRequest)

Validates if the user exists in the database.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **AuthRequest** | [**AuthRequest**](../Models/AuthRequest.md)| Object containing the user&#39;s email and password. | [optional] |

### Return type

[**AuthResponse**](../Models/AuthResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

