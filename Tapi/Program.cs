using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace Tapi
{
    class Program
    {
        static int apiId = 77245;
        static string apiHash = "8bb5f0761bab74261e285f0b691e191b";
        static string user_number = "79393302980";

        static void Main(string[] args)
        {
            Console.Write("0");
            a();
            Console.Write("-1");
            
            Console.ReadKey(true);
        }

        static public async void  a()
        {
            // System.IO.File.Delete("session.dat");
            var session = new FileSessionStore();
            //var p = session.Load("SkySession");
            //p.SessionExpires = 2147483647; ConfigurationManager.AppSettings[
            var client = new TelegramClient( apiId, apiHash,session, "SkySession");
            await client.ConnectAsync();
            if (!client.IsUserAuthorized())
            {
                var hash = await client.SendCodeRequestAsync(user_number);
                Console.Write("Введите код");
                var code = Console.ReadLine(); // you can change code in debugger
                var user = await client.MakeAuthAsync(user_number, hash, code);
            }
            //get available contacts
            var result = await client.GetContactsAsync();
            //var dialogs = await client.GetUserDialogsAsync() As TLDialogResult;
            /*var chat = dialogs.chats.lists
.Where(c => c.GetType() == typeof(TLChannel))
.Cast()
.FirstOrDefault(c => c.username == "<channel_id>");*/
            //Thank you, but not "TLDialogResult". Use "TLDialogsSlice".
            TeleSharp.TL.Contacts.TLRequestImportContacts requestImportContacts = new TeleSharp.TL.Contacts.TLRequestImportContacts();
            requestImportContacts.contacts = new TLVector<TLInputPhoneContact>();
            int num3 = 0;
            //foreach (string str in source)
            {
                requestImportContacts.contacts.lists.Add(new TLInputPhoneContact()
                {
                    phone = "79393986878",//WHO WRITE//WHO WRITE//WHO WRITE//WHO WRITE//WHO WRITE?
                    first_name = num3.ToString(),
                    last_name = num3.ToString()
                });
                ++num3;
            }
            //isregistered 
            var o = client.SendRequestAsync<TeleSharp.TL.Contacts.TLImportedContacts>((TLMethod)requestImportContacts);
            o.Wait();
            var oo=o.Result.users.lists;
            //foreach (object list in (await ) ;

            //TLAbsUpdates tlAbsUpdates = await Program.client.SendMessageAsync((TLAbsInputPeer)tlInputPeerUser1, this.textBox2.Text);



            //var result2 = await client.SearchUserAsync("79518954606");
            //var y = await client.GetUserDialogsAsync() as TLDialogsSlice;
            

            //var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
            //var chat = dialogs.chats.lists
            //    .OfType<TLChannel>()
            //    .FirstOrDefault(c => c.title == "TestGroup");

            //find recipient in contacts

            //var userByPhoneId = await client.ImportContactByPhoneNumber("791812312323"); //import by phone

            //var userByUserNameId = await await client.ImportByUserName("userName"); //import by username

            //send message
            var d=await client.SendMessageAsync(new TLInputPeerUser() { user_id = (oo.First() as TLUser).id }, "123123123");
            var ааа = d;
            Console.ReadKey();
            client.Dispose();
        }
    }
}
