using Microsoft.JSInterop;

namespace RegistroDeHoras.Client.Helpers;

public class BlazorDownloadFile
{
    private readonly IJSRuntime _jsRuntime;
    private readonly MemoryStream _fileStream;
    private readonly string _fileName;
    private readonly string _contentType;

    public BlazorDownloadFile(IJSRuntime jsRuntime, MemoryStream fileStream, string fileName, string contentType)
    {
        _jsRuntime = jsRuntime;
        _fileStream = fileStream;
        _fileName = fileName;
        _contentType = contentType;
    }

    public async Task DownloadFile()
    {
        var fileBytes = _fileStream.ToArray();
        var base64 = Convert.ToBase64String(fileBytes);
        await _jsRuntime.InvokeVoidAsync("BlazorDownloadFile", _fileName, _contentType, base64);
    }
}