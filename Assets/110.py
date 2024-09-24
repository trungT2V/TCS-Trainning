import os

# Thay thế 'folder_path' bằng đường dẫn đến thư mục bạn muốn đổi tên file
folder_path = 'E:/UnityProjects/TCS-Trainning/Assets/AllLabel/UI'

# Lấy danh sách các file trong thư mục
files = os.listdir(folder_path)

# Biến đếm để gán tên file
count = 1

# Duyệt qua từng file trong thư mục
for file_name in files:
    # Lấy phần mở rộng của file
    file_extension = os.path.splitext(file_name)[1]
    
    # Tạo tên file mới với định dạng số
    new_name = f"{count}{file_extension}"
    
    # Đường dẫn đầy đủ của file cũ và file mới
    old_file = os.path.join(folder_path, file_name)
    new_file = os.path.join(folder_path, new_name)
    
    # Đổi tên file
    os.rename(old_file, new_file)
    
    # Tăng biến đếm
    count += 1

print("Đổi tên file thành công!")
