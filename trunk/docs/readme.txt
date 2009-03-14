
sql-smart 介绍

作者：1000copy@gmail.com

1. sql-smart是什么？

sql-smart是一个非常简单的ORM，使用它你可以
  *  不必改变编写sql的习惯
  *  充分利用 codeinsight
  *  充分利用 重构技术

听起来陌生？这是我的实验室项目，并未公开，现在还在实验中，不过现在是可用的状态。

sql-smart和Hibernite有类似之处，都是采用类映射数据库表，字段；
不同的是，掌握后者需要一本书和很多的实践，掌握前者你需要的只是看看这篇文档，然后下载一份去尝试。


2. sql-smart能做什么？

看一个简单的sql对比：

* 纯粹sql 
  string sql = "  select id,name,dept.name  from person left join dept on person.deptid = person.id";

* sql-smart
   sql = "select {0},{1},{2} from {3} left join {4} on {5} ={6} ";
    sql = string.Format(sql, Dept.Name ,Person.name,Person.id, Person, Dept, Person.DeptId, Dept.Id);
  
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
4. sql-smart不做的
   不做完整的映射（比如外键，级联更新），只是简单的表到类，字段到属性。
   这样的好处是简单，并且承认OO不能替换sql，它们根本不能互相替换，而各有强项。
   sql的强项在于
     * 大量的优化
     * 强大的集合操作
     * 强大的数据表达能力
   OO的的强项：
     * 表达操作和封装的能力
     * 现在的重构，重载的支持
   
5. 如何才可以开始?
    sql-smart.dll 提供以上能力（必须）
    sql-smart_autogene.exe 生成类和数据库表字段的对应关系。（没有完成），你可以自己写对应关系，这样并不麻烦。
    看例子可以更快进入状态。
    请从 sql-smart @ google code :http://code.google.com/p/sql-smart/ 下载。
    使用这个类库，你需要有vS2005的安装，当然如果只是阅读，随便什么Notepad就可以。


  
  
  
