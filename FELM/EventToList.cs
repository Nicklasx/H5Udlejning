namespace FELM
{
    class EventToList
    {
        private string _Eventname;
        private string _Name;
        private string _Phone;
        private string _City;
        private string _Street;
        private string _Nr;
        private string _Postal;
        private int _EventCounterId;

        public string EventName
        {
            get { return _Eventname; }
            set { _Eventname = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Street
        {
            get { return _Street; }
            set { _Street = value; }
        }
        public string Nr
        {
            get { return _Nr; }
            set { _Nr = value; }
        }
        public string Postal
        {
            get { return _Postal; }
            set { _Postal = value; }
        }
        public int EventCounterId
        {
            get { return _EventCounterId; }
            set { _EventCounterId = value; }
        }



        public EventToList()
        {

        }

        /*public EventToList(string Event, string Name, string Phone, string City, string Street, string Nr, string Postal)
        {
            this._Eventname = Event;
            this._Name = Name;
            this._Phone = Phone;
            this._City = City;
            this._Street = Street;
            this._Nr = Nr;
            this._Postal = Postal;

        }*/



    }
}
