using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Voucher
{
    public class VoucherData : INotifyPropertyChanged
    {
        #region Notification Changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    
        public int VoucherSerialNumber { get => vouvherSerialNumber; set { vouvherSerialNumber = value; NotifyPropertyChanged(); } }
        private int vouvherSerialNumber;
        public int VoucherPrice { get => voucherPrice; set { voucherPrice = value; NotifyPropertyChanged(); } }
        private int voucherPrice;

        public bool VoucherClient { get => voucherClient; set { voucherClient = value; NotifyPropertyChanged(); } }
        private bool voucherClient;
        public bool VoucherUsed { get => voucherUsed; set { voucherUsed = value; NotifyPropertyChanged(); } }
        private bool voucherUsed;

        public DateTime VoucherCreationDate { get => voucherCreationDate; set { voucherCreationDate = value; NotifyPropertyChanged(); } }
        private DateTime voucherCreationDate;


        private DateTime voucherClientDate;
        public DateTime VoucherClientDate
        {
            get { return voucherClientDate; }
            set { voucherClientDate = value; NotifyPropertyChanged(); }
        }
        private DateTime voucherUsedDate;
        public DateTime VoucherUsedDate
        {
            get { return voucherUsedDate; }
            set { voucherUsedDate = value;  NotifyPropertyChanged(); }
        }
        public bool VoucherVencido { get {
                if ((DateTime.Today - VoucherClientDate).TotalDays > 90 && VoucherClient)
                    return true;
                else
                    return false;
            } }



    }
}
