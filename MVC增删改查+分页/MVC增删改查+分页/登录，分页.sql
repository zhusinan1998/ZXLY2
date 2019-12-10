use master
--判断是否存在同名的数据库
if exists(select *from sys.databases where name='Login')
drop database Login
go
--创建数据库
create database Login
go
use Login
--角色表
if exists(select *from sys.tables where name='Login_role')
drop table Login_role
create table Login_role(
Roleid int not null primary key identity(1,1),--角色id
Rolename varchar(20) not null,--角色名
Roleaction varchar(200) not null,--角色事务
)
go
insert into Login_role values ('村长','管理羊村所有事务')
insert into Login_role values ('村民','安居乐业')
insert into Login_role values('副村长','协助村长')
select *from Login_role
--创建表
if exists(select *from sys.tables where name='Login_Info')
drop table Login_Info
create table Login_Info(
UserId int not null primary key identity(1,1),--用户id
UserName varchar(20) not null,--用户名
UserPwd int not null,--用户密码
Usersex int check(Usersex=0 or Usersex=1),--性别
Usertime datetime default(getdate()),--时间
Useraddress varchar(200),--家庭住址
UserEmail varchar(50),--用户邮箱
Roleid int references Login_Role(Roleid)--角色
)
go
--添加数据
insert into Login_Info values ('慢羊羊',123456,0,getdate(),'青青草原1号','123@qq.com',1)
insert into Login_Info values ('花花',123456,1,getdate(),'青青草原2号','123@qq.com',2)
insert into Login_Info values ('小黑',123456,0,getdate(),'青青草原3号','123@qq.com',2)
insert into Login_Info values ('无限',123456,0,getdate(),'青青草原4号','123@qq.com',2)
insert into Login_Info values ('蓝忘机',123456,0,getdate(),'青青草原5号','123@qq.com',3)
insert into Login_Info values ('魏无羡',123456,0,getdate(),'青青草原6号','123@qq.com',3)
go
--查询全表
select *from Login_Info 
go
--存储过程
--角色查询
if exists (select *from sys.procedures where name='Role_1')
drop proc Role_1
go
create proc Role_1
as
select *from Login_role 
go
exec Login_role
--条件查询
if exists (select *from sys.procedures where name='login_1')
drop proc login_1
go
create proc login_1(@UserName varchar(20),@UserPwd varchar(50))
as
select *from Login_Info where UserName=@UserName and UserPwd=@UserPwd
go
--exec login_1 '麦兜',123456

if exists (select *from sys.procedures where name='login_2')
drop proc login_2
go
create proc login_2(@UserId int)
as
select *from Login_Info where @UserId=UserId 
go
--exec login_2 2
if exists (select *from sys.procedures where name='login_3')
drop proc login_3
go
create proc login_3(@UserName varchar(20))
as
select *from Login_Info where UserName=@UserName
go
exec login_3 '麦兜'
--添加存储
if exists (select *from sys.procedures where name='insert_1')
drop proc insert_1
go
create proc insert_1(@UserName varchar(20),@UserPwd int,@Usersex int,@Useraddress varchar(200),@UserEmail varchar(50),@Roleid int)
as
insert into Login_Info values (@UserName,@UserPwd,@Usersex,getdate(),@Useraddress,@UserEmail,@Roleid)
go
--exec insert_1 '江厌离1',123456,0,'莲花坞','1234@qq.com',2
--更新
if exists (select *from sys.procedures where name='update_1')
drop proc update_1
go
create proc update_1(@UserName varchar(20),@UserPwd int,@Usersex int,@Useraddress varchar(200),@UserEmail varchar(50),@Roleid int,@UserId int)
as
update Login_Info set UserName=@UserName,UserPwd=@UserPwd,Usersex=@Usersex,Useraddress=@Useraddress,UserEmail=@UserEmail ,Roleid=@Roleid where UserId=@UserId
go
--exec update_1 '蓝曦臣','12345',0,'羊村','12@qq.com',2,1
select * from Login_Info where UserName='无限'
--删除
if exists (select *from sys.procedures where name='delete_1')
drop proc delete_1
go
create proc delete_1(@UserId int)
as
delete  Login_Info where @UserId=UserId 
go
--exec delete_1 7
select * from Login_Info where UserName='花花'and UserPwd='123456'

--分页
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
if exists (select *from sys.procedures where name='QueryTruckProc')
drop proc QueryTruckProc
go
CREATE PROCEDURE QueryTruckProc 
	-- Add the parameters for the stored procedure here
	--创建分页参数，pageIndex pageSize TotalCount
   (@PageIndex int=0,
	@PageSize int=2,
	@Number nvarchar(64)='',
	@TotalCount int output ) --输出参数
AS
BEGIN
declare @sqlStr nvarchar(1024)
declare @sqlWhere nvarchar(1024)
set @sqlWhere =''

--判断条件是不是空值
if @Number <> '' And @Number is not null
begin
  set @sqlWhere = @sqlWhere + 'and t.UserName=''' + @Number +''''
end

--定义查询总数的sql
set @sqlStr = 'select count(*) from Login_Info t where 1=1 '+ @sqlWhere;

--定义一个参数,页数
declare @PageCount int;
--执行查询总数的sql
EXEC sp_executesql @sqlStr, N'@RowCount int output',@TotalCount output;

--计算总页数
set @PageCount=CEILING(CONVERT(FLOAT,@TotalCount)/(CONVERT(FLOAT,@PageSize)));

--判断第几页是否在范围之内，

if @PageIndex < 0
begin
set @PageIndex =0 ;
end
if @PageIndex>@PageCount
begin
 set @PageIndex = @PageCount -1;
end

set @sqlStr = 'select *from Login_Info t where  1=1 '+ @sqlWhere +' order by t.UserId offset '+ CONVERT(nvarchar(5) ,(@PageIndex*@PageSize)) +' rows fetch next '+ CONVERT(nvarchar(5),@PageSize) +' rows only '

EXEC sp_executesql @sqlStr
END
GO



