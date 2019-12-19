
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

create table User_info --登陆表
(
	Id int primary key identity(1,1),--编号
	Name varchar(20) not null,--账户
	Pwd varchar(30) not null,--密码
	sex int not null,--性别 1：男；2：女
	Tel int not null,--手机号
	Email varchar(20) not null,--邮箱
	shenfen varchar(40) not null,--身份证号
	addre varchar(50) not null,--住址
	Roles int default(2) not null --角色信息1:管理员；2：用户
)
go

if exists(select * from sys.objects where name='Book_type')
drop table Book_type
go

--书籍类型表
create table Book_type
(
	Btid int primary key identity(1,1),--类型编号
	Btype varchar(20) not null --类型
)



if exists(select * from sys.objects where name='Book_information')
drop table Book_information
go

--书籍信息表
create table Book_information
(
	Bid int primary key identity(1,1),--书籍编号
	Title varchar(20) not null,--书籍名称
	Btid int references Book_type(Btid),--书籍类型 
	Warehousing datetime default(getdate()) not null,--上架时间
	Borrowing_price float not null,--借阅价格/月
	Unit_Price float not null,--单价
	Stock int not null --库存
)
go

if exists(select * from sys.objects where name='Borrowing_records')
drop table Borrowing_records
go

--借阅记录表
create table Borrowing_records
(
	Brid int primary key identity(1,1),--借阅编号
	Id int references User_info(Id) not null,--用户编号
	Lend_time datetime default(getdate()) not null,--借出时间
	Return_time datetime default(getdate()) not null --归还时间
)
go

if exists(select * from sys.objects where name='Sales_list')
drop table Sales_list
go
--销售记录表
create table Sales_list
(
	Slid int primary key identity(1,1),--销售编号
	Bid int references Book_information(Bid) not null,--书籍编号
	Id int references User_info(Id) not null,--用户编号
	purchase_time datetime default(getdate())--售出时间
)
go

--数据插入
--插入角色表
insert into User_info values('admin','admin','1','213456','123@qq.com','410622198203028521','北京','1')
insert into User_info values('张三','123456','1','12356','123@qq.com','410622198203028521','北京','2')

--插入类型表
insert into Book_type values('小说')
insert into Book_type values('文学')
insert into Book_type values('历史')

--插入书籍信息表
insert into Book_information values('三国演义','1','2019-05-05','5','50','10')
insert into Book_information values('水浒传','1','2019-05-05','5','50','10')
insert into Book_information values('西游记','1','2019-05-05','5','50','10')
insert into Book_information values('红楼梦','1','2019-05-05','5','50','10')
insert into Book_information values('万历十五年','3','2019-05-05','5','40','10')

--插入借阅表
insert into Borrowing_records values('2','2019-11-06','2019-12-06')
insert into Borrowing_records values('2','2019-11-06','2019-12-06')
insert into Borrowing_records values('2','2019-11-06','2019-12-06')

--插入销售表
insert into Sales_list values ('1','2','2012-11-11')
insert into Sales_list values ('2','2','2012-11-11')
insert into Sales_list values ('3','2','2012-11-11')

--角色表查询
select * from User_info where Id=2 and Pwd='123456'
--类型表查询
select * from Book_type
--书籍信息表查询
select * from Book_type,Book_information where Book_type.Btid = Book_information.Btid
--借阅表查询
select * from User_info,Borrowing_records where User_info.Id = Borrowing_records.Id
--销售表查询
select * from Book_information,Sales_list where Book_information.Bid = Sales_list.Bid
