use master
--�ж��Ƿ����ͬ�������ݿ�
if exists(select *from sys.databases where name='Login')
drop database Login
go
--�������ݿ�
create database Login
go
use Login
--��ɫ��
if exists(select *from sys.tables where name='Login_role')
drop table Login_role
create table Login_role(
Roleid int not null primary key identity(1,1),--��ɫid
Rolename varchar(20) not null,--��ɫ��
Roleaction varchar(200) not null,--��ɫ����
)
go
insert into Login_role values ('�峤','���������������')
insert into Login_role values ('����','������ҵ')
insert into Login_role values('���峤','Э���峤')
select *from Login_role
--������
if exists(select *from sys.tables where name='Login_Info')
drop table Login_Info
create table Login_Info(
UserId int not null primary key identity(1,1),--�û�id
UserName varchar(20) not null,--�û���
UserPwd int not null,--�û�����
Usersex int check(Usersex=0 or Usersex=1),--�Ա�
Usertime datetime default(getdate()),--ʱ��
Useraddress varchar(200),--��ͥסַ
UserEmail varchar(50),--�û�����
Roleid int references Login_Role(Roleid)--��ɫ
)
go
--�������
insert into Login_Info values ('������',123456,0,getdate(),'�����ԭ1��','123@qq.com',1)
insert into Login_Info values ('����',123456,1,getdate(),'�����ԭ2��','123@qq.com',2)
insert into Login_Info values ('С��',123456,0,getdate(),'�����ԭ3��','123@qq.com',2)
insert into Login_Info values ('����',123456,0,getdate(),'�����ԭ4��','123@qq.com',2)
insert into Login_Info values ('������',123456,0,getdate(),'�����ԭ5��','123@qq.com',3)
insert into Login_Info values ('κ����',123456,0,getdate(),'�����ԭ6��','123@qq.com',3)
go
--��ѯȫ��
select *from Login_Info 
go
--�洢����
--��ɫ��ѯ
if exists (select *from sys.procedures where name='Role_1')
drop proc Role_1
go
create proc Role_1
as
select *from Login_role 
go
exec Login_role
--������ѯ
if exists (select *from sys.procedures where name='login_1')
drop proc login_1
go
create proc login_1(@UserName varchar(20),@UserPwd varchar(50))
as
select *from Login_Info where UserName=@UserName and UserPwd=@UserPwd
go
--exec login_1 '��',123456

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
exec login_3 '��'
--��Ӵ洢
if exists (select *from sys.procedures where name='insert_1')
drop proc insert_1
go
create proc insert_1(@UserName varchar(20),@UserPwd int,@Usersex int,@Useraddress varchar(200),@UserEmail varchar(50),@Roleid int)
as
insert into Login_Info values (@UserName,@UserPwd,@Usersex,getdate(),@Useraddress,@UserEmail,@Roleid)
go
--exec insert_1 '������1',123456,0,'������','1234@qq.com',2
--����
if exists (select *from sys.procedures where name='update_1')
drop proc update_1
go
create proc update_1(@UserName varchar(20),@UserPwd int,@Usersex int,@Useraddress varchar(200),@UserEmail varchar(50),@Roleid int,@UserId int)
as
update Login_Info set UserName=@UserName,UserPwd=@UserPwd,Usersex=@Usersex,Useraddress=@Useraddress,UserEmail=@UserEmail ,Roleid=@Roleid where UserId=@UserId
go
--exec update_1 '���س�','12345',0,'���','12@qq.com',2,1
select * from Login_Info where UserName='����'
--ɾ��
if exists (select *from sys.procedures where name='delete_1')
drop proc delete_1
go
create proc delete_1(@UserId int)
as
delete  Login_Info where @UserId=UserId 
go
--exec delete_1 7
select * from Login_Info where UserName='����'and UserPwd='123456'

--��ҳ
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
	--������ҳ������pageIndex pageSize TotalCount
   (@PageIndex int=0,
	@PageSize int=2,
	@Number nvarchar(64)='',
	@TotalCount int output ) --�������
AS
BEGIN
declare @sqlStr nvarchar(1024)
declare @sqlWhere nvarchar(1024)
set @sqlWhere =''

--�ж������ǲ��ǿ�ֵ
if @Number <> '' And @Number is not null
begin
  set @sqlWhere = @sqlWhere + 'and t.UserName=''' + @Number +''''
end

--�����ѯ������sql
set @sqlStr = 'select count(*) from Login_Info t where 1=1 '+ @sqlWhere;

--����һ������,ҳ��
declare @PageCount int;
--ִ�в�ѯ������sql
EXEC sp_executesql @sqlStr, N'@RowCount int output',@TotalCount output;

--������ҳ��
set @PageCount=CEILING(CONVERT(FLOAT,@TotalCount)/(CONVERT(FLOAT,@PageSize)));

--�жϵڼ�ҳ�Ƿ��ڷ�Χ֮�ڣ�

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



