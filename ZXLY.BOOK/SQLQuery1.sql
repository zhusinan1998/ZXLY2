
if exists(select * from sys.databases where name='Bookstore')
drop database Bookstore
go

create database Bookstore
go

use Bookstore
go

if exists(select * from sys.objects where name='User_info')
drop table User_info
go

create table User_info --��½��
(
	Id int primary key identity(1,1),--���
	Name varchar(20) not null,--�˻�
	Pwd varchar(30) not null,--����
	sex int not null,--�Ա� 1���У�2��Ů
	Tel int not null,--�ֻ���
	Email varchar(20) not null,--����
	shenfen varchar(40) not null,--���֤��
	addre varchar(50) not null,--סַ
	Roles int default(2) not null --��ɫ��Ϣ1:����Ա��2���û�
)
go

if exists(select * from sys.objects where name='Book_type')
drop table Book_type
go

--�鼮���ͱ�
create table Book_type
(
	Btid int primary key identity(1,1),--���ͱ��
	Btype varchar(20) not null --����
)



if exists(select * from sys.objects where name='Book_information')
drop table Book_information
go

--�鼮��Ϣ��
create table Book_information
(
	Bid int primary key identity(1,1),--�鼮���
	Title varchar(20) not null,--�鼮����
	Btid int references Book_type(Btid),--�鼮���� 
	Warehousing datetime default(getdate()) not null,--�ϼ�ʱ��
	Borrowing_price float not null,--���ļ۸�/��
	Unit_Price float not null,--����
	Stock int not null --���
)
go

if exists(select * from sys.objects where name='Borrowing_records')
drop table Borrowing_records
go

--���ļ�¼��
create table Borrowing_records
(
	Brid int primary key identity(1,1),--���ı��
	Id int references User_info(Id) not null,--�û����
	Lend_time datetime default(getdate()) not null,--���ʱ��
	Return_time datetime default(getdate()) not null --�黹ʱ��
)
go

if exists(select * from sys.objects where name='Sales_list')
drop table Sales_list
go
--���ۼ�¼��
create table Sales_list
(
	Slid int primary key identity(1,1),--���۱��
	Bid int references Book_information(Bid) not null,--�鼮���
	Id int references User_info(Id) not null,--�û����
	purchase_time datetime default(getdate())--�۳�ʱ��
)
go

--���ݲ���
--�����ɫ��
insert into User_info values('admin','admin','1','213456','123@qq.com','410622198203028521','����','1')
insert into User_info values('����','123456','1','12356','123@qq.com','410622198203028521','����','2')

--�������ͱ�
insert into Book_type values('С˵')
insert into Book_type values('��ѧ')
insert into Book_type values('��ʷ')

--�����鼮��Ϣ��
insert into Book_information values('��������','1','2019-05-05','5','50','10')
insert into Book_information values('ˮ䰴�','1','2019-05-05','5','50','10')
insert into Book_information values('���μ�','1','2019-05-05','5','50','10')
insert into Book_information values('��¥��','1','2019-05-05','5','50','10')
insert into Book_information values('����ʮ����','3','2019-05-05','5','40','10')

--������ı�
insert into Borrowing_records values('2','2019-11-06','2019-12-06')
insert into Borrowing_records values('2','2019-11-06','2019-12-06')
insert into Borrowing_records values('2','2019-11-06','2019-12-06')

--�������۱�
insert into Sales_list values ('1','2','2012-11-11')
insert into Sales_list values ('2','2','2012-11-11')
insert into Sales_list values ('3','2','2012-11-11')

--��ɫ���ѯ
select * from User_info where Id=2 and Pwd='123456'
--���ͱ��ѯ
select * from Book_type
--�鼮��Ϣ���ѯ
select * from Book_type,Book_information where Book_type.Btid = Book_information.Btid
--���ı��ѯ
select * from User_info,Borrowing_records where User_info.Id = Borrowing_records.Id
--���۱��ѯ
select * from Book_information,Sales_list where Book_information.Bid = Sales_list.Bid
