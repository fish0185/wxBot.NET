﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wxBot.NET
{
    class SimpleWXbot:wxbot
    {
        public override void handle_msg_all(wxMsg msg)
        {
            //if (msg.Type == 4 && msg.ContentType == 0)
            //{
                string uid = get_user_id("101");
                send_msg_by_uid("test,do not reply", uid);
           // }
        }
    }
}
