
sql-smart 介绍

作者：1000copy@gmail.com

1. sql-smart是什么？

sql-smart是一个DotNet类库，它可以充分利用 codeinsight ,面向对象技术来帮助程序员更快的，更准确到编写sql，这样的sql以容易重构的。
听起来陌生？这是想法我之前并没有在其他ORM技术，比如 Hibernate,ROR内这样大名鼎鼎到类库中见过（如果你发现这并不确切，请给我打个招呼）。
因此，这并不是一个Yet Another的类库。
不过它不像是听起来那么复杂，实际上，它的主要特点就是简单。我会继续详细的说明，包括必要到例子。
鉴于简单，也不需要特定到语言特色的支持，因此，在其他语言中一样可以实现。
不过，我发现C#的模板技术，和RTTI 让代码编写更加容易，并且VS到code insight技术让sql-smart工作良好。
实际上我的实现正是基于C#来做的。

sql-smart和Hibernite有类似之处，都是采用类表达数据库，不同的是，掌握后者需要一本书和很多的实践，掌握前者你需要的只是看看这篇文档，然后下载一份去尝试。


2. sql-smart能做什么？

用一个例子来说明吧。
如果给你一个数据库，内有表两张。一个Dept，一个Person，这是我们比较常见到表，他们可以有关联关系，比如Person和Dept有多对一的关系。
它们到创建sql为：

  drop table Person
  drop table Dept
  create table Person
  (
    id int,
    DeptId int,
    name varchar(10),
	birthday datetime
  )
  create table Dept
  (
    id int,
    name varchar(10)
  )
  那么对它到常用操作比如查询PersonList : 
    sql = "select d.name as deptname,p.name,p.id from person  p left join dept d on p.deptid = d.id"
  这对大家来说是再熟悉不过的了。对我也是如此，我自从第一次使用sqlserver就是这么干的，干了很多年了。
  那么在sql-smart内将会是
  
    string sql = "";
    sql = "select {0},{1},{2} from {3} left join {4} on {5} ={6} ";
    sql = string.Format(sql, Dept.Name ,Person.name,Person.id, Person, Dept, Person.DeptId, Dept.Id);
  差别不大，实际上sql-smart的关键技术不去改变写sql到过程。不想Hibernian，ROR以类为核心，而Linq看起来像是sql，实际上不是。有些很细微到，麻烦的模式切换。
  我的观点一向是sql是工业标准，围绕着sql去做工作，而不是另外建立一套专有方案，会让迁移模式变得简单。
  那么好处是什么？
  
3. 更快，更加精确，不必改变现在的习惯。

   3.1 一般sql是基于字符串的，无法充分利用语言的编译特性去检查错误。
   sql-smart可以更好到利用编译器技术去检错。
   3.2 当重构的时候，知道表，字段在那里被引用了至关重要。
   使用Pure sql要查询应用，只能使用Find，不精确。sql-smart因为用类来表达表和字段，想要查找应用就直接用VS到Find References即可。
   3.3 当需要改名的时候，sql-smart比较方便的改名。
   使用VS有些2年了，我常常发现在编程过程中，引入了新的特性的时候，改名以便支持更好的符合新的需求，是常常需要的。
   需要在Pure sql内要改名，需要用find/replace ,改了之后需要测试，常常会出现改错到情况。
   而采用sql-smart来产生Sql要修改表名，字段名，直接用F2搞定，不必测试，只要VS让你改，就不会错：逻辑上改名和原来的代码是完全等效的。
   3.4 Pure sql无法利用VS的代码提示，而sql-smart可以。
    比如 Dept.Name ,Person.name,Person.id，这里到Person,Dept,Name,Id都是可以代码提示的。   
   3.5 Linq,ROR,Hibernate都需要改变程序员到习惯，sql-smart的改变很小。
   看上面提供的对比例子就知道这一点是OK的。
   
   
   采用sql-smart可以更好到利用语言到编译能力去检错，并且充分利用VS提供的重构，代码提示功能。
   
4. 如何才可以开始?
    sql-smart.dll 提供以上能力（必须）
    sql-smart_autogene.exe 生成类和数据库表字段的对应关系。（可选），你可以自己写对应关系，这样并不麻烦。
    看例子可以更快进入状态。
    请从 sql-smart @ google code :http://code.google.com/p/sql-smart/ 下载。
    使用这个类库，你需要有vS2008的安装，当然如果只是阅读，随便什么Notepad就可以。

  
  
  
