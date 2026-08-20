[`< Back`](./)

---

# SSHCommand

Namespace: BurnSoft.SecureShell

Connect to a host and execute a ssh command on that host

```csharp
public class SSHCommand
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SSHCommand](./burnsoft.secureshell.sshcommand)

## Constructors

### **SSHCommand()**

```csharp
public SSHCommand()
```

## Methods

### **RunCommand(String, String, String, String, String&)**

Runs the command.

```csharp
public static string RunCommand(string host, string uid, string pwd, string cmd, String& errOut)
```

#### Parameters

`host` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The host.

`uid` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The uid.

`pwd` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The password.

`cmd` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The command.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
System.String.

### **SSHAlive(String, String, String, String&)**

SSHs the alive.

```csharp
public static bool SSHAlive(string host, string uid, string pwd, String& errOut)
```

#### Parameters

`host` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The host.

`uid` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The uid.

`pwd` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The password.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

### **DeviceIsUp(String, String&, Int32)**

Devices the is up.

```csharp
public static bool DeviceIsUp(string host, String& errOut, int timeout)
```

#### Parameters

`host` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The host.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

`timeout` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The timeout.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
No Host or IP Listed!

---

[`< Back`](./)
