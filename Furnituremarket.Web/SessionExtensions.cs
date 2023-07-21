﻿using Furnituremarket.Domain.ViewModels.Cart;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace Furnituremarket.Web
{
    public static class SessionExtensions
    {
        private const string key = "Cart";
        public static void Set(this ISession session, CartViewModel value) 
        {
            if (value == null)
                return;

            using(var stream = new MemoryStream())
            using(var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(value.OrderId);
                writer.Write(value.TotalCount);
                writer.Write(value.TotalPrice);
               
                session.Set(key,stream.ToArray());
            }
        }

        public static bool TryGetCart(this ISession session, out CartViewModel value) 
        {
            if(session.TryGetValue(key, out byte[] buffer))
            {
                using(var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var orderId = reader.ReadInt32();
                    var totalCount = reader.ReadInt32();
                    var totalPrice = reader.ReadDecimal();

                    value = new CartViewModel(orderId)
                    {
                        TotalCount = totalCount,
                        TotalPrice = totalPrice
                    };

                    return true;
                }
            }
            value = null;
            return false;
        }
    }
}
