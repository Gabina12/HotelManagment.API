using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagment.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descrip { get; set; }
        public int Floor { get; set; }
        public int? RoomsCount { get; set; }
        public int RoomTypeId { get; set; }
        public decimal PricePerDay { get; set; }

        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomTypes { get; set; }
        public virtual ICollection<RoomAttribute> RoomAttributes { get; set; }
    }

    public class RoomType {
        public int Id { get; set; }
        public string Descrip { get; set; }
    }

    public class AttributeCode {
        public int Id { get; set; }
        public string AttribCode { get; set; }
        public string AttribDescrip { get; set; }
    }

    public class RoomAttribute {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string AttribCode { get; set; }
        public string AttribValue { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Rooms { get; set; }
    }

    public class Operation {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime OperationDate { get; set; }
        public int ReservedDaies { get; set; }
        public DateTime OperationEndDate { get { return OperationDate.AddDays(ReservedDaies); } }
        public decimal TotalPrice { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Rooms { get; set; }
    }

    public class Sale {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal NewPrice { get; set; }
        public int SaleTypeId { get; set; }
        public decimal SaleValue { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Rooms { get; set; }
        [ForeignKey("SaleTypeId")]
        public virtual SaleType SaleTypes { get; set; }
    }

    public class SaleType {
        public int Id { get; set; }
        public string Descrip { get; set; }
    }
}
