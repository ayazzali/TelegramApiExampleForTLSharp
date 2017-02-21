using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core;
using TeleSharp.TL;

namespace ConsoleApplication1
{
    class Program
    {
        static int apiId = 00000;//https://my.telegram.org/auth?to=apps
        static string apiHash = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";//https://my.telegram.org/auth?to=apps
        static string user_number = "xxxxxxxxxxx";//Your Phone Number
        static string phoneNumber = user_number;
        static void Main(string[] args)
        {
            a() ;
            Console.ReadKey();
        }
        static async void a()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(apiId, apiHash, store, "session");// or C:\\Temp\\mySession
            await client.ConnectAsync();
            if (!client.IsUserAuthorized())
            {
                var hash = await client.SendCodeRequestAsync(phoneNumber); //отсылаем запрос на создании сессии

                var code = Console.ReadLine(); // код который придет от Telegram 

                var user = await client.MakeAuthAsync(phoneNumber, hash, code); // создаем сессию
                
            }

            //var userByPhoneId = await client.("791812312323"); // импорт по номеру телефона
            //var userByUserNameId = await await client.ImportByUserName("userName"); // импорт по юзернейму

            var s = await client.IsPhoneRegisteredAsync("xxxxxxxxxxx");//WHO WRITE//WHO WRITE

            var f = await client.GetContactsAsync();
            Console.WriteLine("контакты");
            f.contacts.lists.ForEach(_ => { Console.WriteLine(_.user_id); });
            
            TeleSharp.TL.Contacts.TLRequestImportContacts requestImportContacts = new TeleSharp.TL.Contacts.TLRequestImportContacts();
            requestImportContacts.contacts = new TLVector<TLInputPhoneContact>();
            requestImportContacts.contacts.lists.Add(new TLInputPhoneContact()
            {
                phone = "xxxxxxxxxxx",//WHO WRITE//WHO WRITE//WHO WRITE//WHO WRITE//WHO WRITE?
                first_name = "",
                last_name = ""
            });
            var o = await client.SendRequestAsync<TeleSharp.TL.Contacts.TLImportedContacts>((TLMethod)requestImportContacts);
            var NewUserId = (o.users.lists.First() as TLUser).id;
            var d = await client.SendMessageAsync(new TLInputPeerUser() { user_id = NewUserId }, "text xxx");
            //return true;
        }
    }
}
