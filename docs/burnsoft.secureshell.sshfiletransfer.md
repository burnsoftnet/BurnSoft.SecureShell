[`< Back`](./)

---

# SSHFileTransfer

Namespace: BurnSoft.SecureShell

Class that handles the Secure FIle Transfer to the selected host

```csharp
public class SSHFileTransfer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SSHFileTransfer](./burnsoft.secureshell.sshfiletransfer)

## Constructors

### **SSHFileTransfer()**

```csharp
public SSHFileTransfer()
```

## Methods

### **SendDebug(String)**

Sends the debug.

```csharp
protected void SendDebug(string value)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value.

### **OnUploadStatus(Int32)**

Called when [upload status].

```csharp
protected void OnUploadStatus(int e)
```

#### Parameters

`e` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The e.

### **OnDownloadStatus(Int32)**

Called when [download status].

```csharp
protected void OnDownloadStatus(int e)
```

#### Parameters

`e` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The e.

### **OnCurrentFile(String)**

Called when [current file].

```csharp
protected void OnCurrentFile(string e)
```

#### Parameters

`e` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The e.

### **DownloadFile(String, String, String, String, String, String&)**

Downloads the file.

```csharp
public bool DownloadFile(string host, string uid, string pwd, string remoteFileAndPath, string localFileAndPath, String& errOut)
```

#### Parameters

`host` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The host.

`uid` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The uid.

`pwd` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The password.

`remoteFileAndPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The remote file and path.

`localFileAndPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The local file and path.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

### **DownloadDirectory(String, String, String, String, String, String&)**

Downloads the directory.

```csharp
public bool DownloadDirectory(string host, string uid, string pwd, string remotePath, string localPath, String& errOut)
```

#### Parameters

`host` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The host.

`uid` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The uid.

`pwd` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The password.

`remotePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The remote path.

`localPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The local path.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

### **UploadFile(String, String, String, String, String, String, String&)**

Uploads the file from the local machine to the remote machine..

```csharp
public bool UploadFile(string host, string uid, string pwd, string remotePath, string localPath, string fileName, String& errOut)
```

#### Parameters

`host` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The host.

`uid` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The uid.

`pwd` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The password.

`remotePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The remote path.

`localPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The local path.

`fileName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the file.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

## Events

### **UploadStatus**

Occurs when [upload status].

```csharp
public event EventHandler<int> UploadStatus;
```

### **DownloadStatus**

Occurs when [download status].

```csharp
public event EventHandler<int> DownloadStatus;
```

### **CurrentFile**

Occurs when [current file].

```csharp
public event EventHandler<string> CurrentFile;
```

### **DebugInformation**

Occurs when [debug information].

```csharp
public event EventHandler<string> DebugInformation;
```

---

[`< Back`](./)
