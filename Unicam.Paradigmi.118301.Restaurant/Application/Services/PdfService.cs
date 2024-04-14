using Application.Models.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PdfService
{
        public byte[] GeneratePdfFromOrders(List<OrderDTO> orders)
        {
            byte[] pdfInBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfWriter.GetInstance(document, ms);
                document.Open();
                foreach (var order in orders)
                {
                   
                    document.Add(new Paragraph($"User ID: {order.UserId}, Date: {order.OrderDate}," +
                        $" Total Check: {order.TotalCheck}, Delivery Address: {order.DeliveryAddress}"));
                    document.Add(Chunk.NEWLINE);

                    foreach (var dish in order.OrderedDishes)
                    { 
                        document.Add(new Paragraph($"Dish: {dish.Name}, Price: {dish.Price}," +
                        $" Quantity: {dish.Quantity}, Type: {dish.Type}"));
                    }

                    document.Add(Chunk.NEWLINE);
                }
                document.Close();
                pdfInBytes = ms.ToArray();
            }
            return pdfInBytes;
        }
    }
}
