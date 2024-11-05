# UserApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**addUser**](UserApi.md#addUser) | **POST** /api/user | Adds a new user. |
| [**getAllUsers**](UserApi.md#getAllUsers) | **GET** /api/user | Retrieves all users. |
| [**getUserByEmail**](UserApi.md#getUserByEmail) | **GET** /api/user/{email} | Retrieves a user by their email address. |
| [**updateUser**](UserApi.md#updateUser) | **PUT** /api/user/{email} | update a user by their email address. |


<a name="addUser"></a>
# **addUser**
> UserResponse addUser(UserRequest)

Adds a new user.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **UserRequest** | [**UserRequest**](../Models/UserRequest.md)| User to add to the system. | [optional] |

### Return type

[**UserResponse**](../Models/UserResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

<a name="getAllUsers"></a>
# **getAllUsers**
> List getAllUsers()

Retrieves all users.

### Parameters
This endpoint does not need any parameter.

### Return type

[**List**](../Models/UserResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="getUserByEmail"></a>
# **getUserByEmail**
> UserResponse getUserByEmail(email)

Retrieves a user by their email address.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **email** | **String**| The email address of the user to retrieve. | [default to null] |

### Return type

[**UserResponse**](../Models/UserResponse.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, text/json

<a name="updateUser"></a>
# **updateUser**
> updateUser(email, UserUpdateRequest)

update a user by their email address.

### Parameters

|Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **email** | **String**| The email address of the user to update. | [default to null] |
| **UserUpdateRequest** | [**UserUpdateRequest**](../Models/UserUpdateRequest.md)| User to update from the system. | [optional] |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: text/plain, application/json, text/json

