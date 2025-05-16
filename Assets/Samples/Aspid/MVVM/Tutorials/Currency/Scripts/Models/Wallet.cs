using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.Currency.Models
{
    public sealed class Wallet
    {
        public event Action<int> SoftChanged;
        public event Action<int> HardChanged;
    
        private int _soft;
        private int _hard;
    
        public int Soft
        {
            get => _soft;
            private set 
            {
                _soft = value;
                SoftChanged?.Invoke(value);
            }
        }
    
        public int Hard
        {
            get => _hard;
            private set
            {
                _hard = value;
                HardChanged?.Invoke(value);
            }
        }

        public Wallet(int soft, int hard)
        {
            ThrowExceptionIfValueLessThanZero(soft);
            ThrowExceptionIfValueLessThanZero(hard);
            
            _soft = soft;
            _hard = hard;
        }

        public void PutSoft(int soft)
        {
            ThrowExceptionIfValueLessThanZero(soft);
            Soft += soft;
        }
    
        public void PutHard(int hard)
        {
            ThrowExceptionIfValueLessThanZero(hard);
            Hard += hard;
        }
    
        public bool TryGetSoft(int soft)
        {
            if (Soft < soft) return false;
        
            Soft -= soft;
            return true;
        }
    
        public bool TryGetHard(int hard)
        {
            if (Hard < hard) return false;
    
            Hard -= hard;
            return true;
        }

        private static void ThrowExceptionIfValueLessThanZero(int value, [CallerMemberName] string methodName = "")
        {
            if (value < 0) 
                throw new Exception($"[{nameof(Wallet)}] [{methodName}] Value cannot be less than 0");
        }
    }
}