using Furnituremarket.Web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace Furnituremarket.Web
{
    public static class SessionExtensions
    {
        private const string key = "Cart";
        public static void Set(this ISession session, Cart value) 
        {
            if (value == null)
                return;

            using(var stream = new MemoryStream())
            using(var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(value.Items.Count);
                foreach(var item in value.Items)
                {
                    writer.Write(item.Key);
                    writer.Write(item.Value);
                }
                writer.Write(value.Amount);
                session.Set(key,stream.ToArray());
            }
        }

        public static bool TryGetCart(this ISession session,out Cart value) 
        {
            if(session.TryGetValue(key, out byte[] buffer))
            {
                using(var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    value = new Cart();
                    var length = reader.ReadInt32();
                    for (int i = 0; i < length; i++)
                    {
                        var furnitureId = reader.ReadInt32();
                        var count = reader.ReadInt32();

                        value.Items.Add(furnitureId, count);
                    }
                    value.Amount = reader.ReadDecimal();

                    return true;
                }
            }
            value = null;
            return false;
        }
    }
}
