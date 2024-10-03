# Demo project

## Components

- QTI Player (Main Interface)
- Package Manager
- Converter
- QTI Provider
- DB

![Documentation Diagram](Documentation.drawio.svg)

## Summary

### Main Interface

As a name suggests this is the main interface of the Demo project which should support both the use cases of employees and students as well.
For the case of an employee, we need to support uploading the qti packages to the database, but this should happen only through Package Manager.
Tests ready for the students should be cached on the main interface s erver, so that a need to constantly access DB diminishes.

### Package Manager

Package Manager must be an interface between the Provider and the Main Interface and is the only way to upload testitems in the DB.
This components also has the ability to scrape data from the package and if necessary convert to QTI3.0 before uploading. (QTI3.0 is mandatory version to be stored in the DB)

### Converter

Pre-built component for converting QTI2.1, QTI2.2 to QTI3.0.

### Provider

This is the DAL (Data Access Layer) of the Demo Project. Access to the Database happens only through this component. Can be considered as the most complex back-end component in this model. Provider should open access to the database, group testitems into the tests. provide single tests or whole testitems as well. Providing testitem happens by responsing with the QTI3.0 package, but once tests are requested GUID of each testitem is returned.

### DB

Data will be hosted on the AWS S3 Bucket service.

## API Documentation

### Package Manager

#### POST /PackageInfo

##### Description
This endpoint opens and scrapes data from the provided QTI package.zip file and returns information about the package, such as its version and other relevant details.

##### URL
`/PackageInfo`

##### Method
`POST`

##### Request
- **Headers:**
    - `Content-Type: multipart/form-data`
- **Body:**
    - `file`: The QTI package.zip file to be uploaded.

##### Responses

- **Success Response:**
    - **Code:** 200 OK
    - **Content:**
        ```json
        {
            "version": "QTI3.0",
            "packageName": "Sample Package",
            "numberOfItems": 10,
            "createdDate": "2023-10-01T12:00:00Z",
            "author": "John Doe"
        }
        ```

- **Error Responses:**
    - **Code:** 400 Bad Request
        - **Content:**
            ```json
            {
                "error": "Invalid file format. Please upload a valid QTI package.zip file."
            }
            ```
    - **Code:** 500 Internal Server Error
        - **Content:**
            ```json
            {
                "error": "An error occurred while processing the package."
            }
            ```

##### Example cURL
```sh
curl -X POST http://example.com/PackageInfo \
    -H "Content-Type: multipart/form-data" \
    -F "file=@/path/to/package.zip"
```

#### POST /PackageUpload

##### Description
This endpoint uploads a QTI package to the database. If the provided package is not in QTI 3.0 format, it can be converted to QTI 3.0 before uploading if specified in the request. If conversion is not specified, an appropriate error is returned.

##### URL
`/PackageUpload`

##### Method
`POST`

##### Request
- **Headers:**
    - `Content-Type: multipart/form-data`
- **Body:**
    - `file`: The QTI package.zip file to be uploaded.
    - `convertToQTI3`: (optional) Boolean flag indicating whether to convert the package to QTI 3.0 if it is not already in that format.

##### Responses

- **Success Response:**
    - **Code:** 200 OK
    - **Content:**
        ```json
        {
            "message": "Package uploaded successfully.",
            "packageUrl": "http://example.com/package/12345"
        }
        ```

- **Error Responses:**
    - **Code:** 400 Bad Request
        - **Content:**
            ```json
            {
                "error": "Invalid file format. Please upload a valid QTI package.zip file."
            }
            ```
    - **Code:** 400 Bad Request
        - **Content:**
            ```json
            {
                "error": "Package is not in QTI 3.0 format. Please specify 'convertToQTI3' to convert and upload."
            }
            ```
    - **Code:** 500 Internal Server Error
        - **Content:**
            ```json
            {
                "error": "An error occurred while processing the package."
            }
            ```

##### Example cURL
```sh
curl -X POST http://example.com/PackageUpload \
    -H "Content-Type: multipart/form-data" \
    -F "file=@/path/to/package.zip" \
    -F "convertToQTI3=true"
```