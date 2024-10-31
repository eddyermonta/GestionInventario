# UserApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addUser**](UserApi.md#addUser) | **POST** /api/user |  |
| [**getAllUsers**](UserApi.md#getAllUsers) | **GET** /api/user |  |
| [**getUserByEmail**](UserApi.md#getUserByEmail) | **GET** /api/user/{email} |  |
| [**updateUser**](UserApi.md#updateUser) | **PUT** /api/user/{email} |  |


<a name="addUser"></a>
# **addUser**
> UserDto addUser(UserDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **UserDto** | [**UserDto**](../Models/UserDto.md)|  | [optional] |

### Return type

[**UserDto**](../Models/UserDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="getAllUsers"></a>
# **getAllUsers**
> List getAllUsers()



### Parameters
This endpoint does not need any parameter.

### Return type

[**List**](../Models/UserDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getUserByEmail"></a>
# **getUserByEmail**
> UserDto getUserByEmail(email)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **email** | **String**|  | [default to null] |

### Return type

[**UserDto**](../Models/UserDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateUser"></a>
# **updateUser**
> updateUser(email, UserUpdateDto)



### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **email** | **String**|  | [default to null] |
| **UserUpdateDto** | [**UserUpdateDto**](../Models/UserUpdateDto.md)|  | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

