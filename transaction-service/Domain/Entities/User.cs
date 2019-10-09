using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Domain.Entities
{
    [Table("ims_user")]
    public class User
    {
        [Column("ims_user_id")]
        public int UserId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("password_salt")]
        public string PasswordSalt { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Column("failed_attempts")]
        public int FailedAttempts { get; set; }
        [Column("locked_until")]
        public DateTime LockedUntil { get; set; }

        // public DTO.Client toDto()
        // {
        //     return new DTO.Client(){
        //         ClientId = ClientId,
        //         Name = Name,
        //         Description = Description
        //     };
        // }
    }
}