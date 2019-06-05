using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OpenSchool.Models
{
    public class MensajeChat
    {
        public MensajeChat()
        {
            status = messageStatus.Sent;
        }

        public enum messageStatus
        {
            Sent,
            Delivered
        }
        [Key]
        public int id { get; set; }
        public string sender_id { get; set; }
        public string receiver_id { get; set; }
        public string message { get; set; }
        public messageStatus status { get; set; }
        public DateTime created_at { get; set; }

        public virtual UsuariosAsp Usuarios { get; set; }

        public virtual UsuariosAsp Usuarios1 { get; set; }
    }
}