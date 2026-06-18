namespace Web_api.ViewModels.Common
{
    public class DTResult<T>
    {
        // Số thứ tự request (DataTables gửi lên bao nhiêu thì trả về bấy nhiêu)
        public int Draw { get; set; }
        // Tổng số dòng thực tế trong Database khi chưa lọc
        public int RecordsTotal { get; set; }
        // Tổng số dòng sau khi đã lọc (Nếu không tìm kiếm thì bằng recordsTotal)
        public int RecordsFiltered { get; set; }
        // Mảng chứa danh sách dữ liệu trả về để đổ lên bảng
        public List<T> Data { get; set; } = new List<T>();

        /* 
            Draw              Số lần request của DataTables
            RecordsTotal      Tổng số bản ghi trong DB
            RecordsFiltered   Tổng số bản ghi sau khi search/filter
            Data              Danh sách dữ liệu trả về cho bảng
        */
    }
}
