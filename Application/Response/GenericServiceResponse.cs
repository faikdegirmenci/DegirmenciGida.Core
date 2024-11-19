using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class GenericServiceResponse<T>
    {
        public bool Success { get; set; }       // Operasyonun başarılı olup olmadığını gösterir
        public string Message { get; set; }     // Başarı veya hata mesajını içerir
        public T Data { get; set; }             // Operasyonun döndürdüğü veri
        public List<string> Errors { get; set; } = new List<string>();  // Hata mesajları listesi

        public GenericServiceResponse()
        {
        }

        public GenericServiceResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public GenericServiceResponse(bool success, string message, List<string> errors)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }
    }
}
