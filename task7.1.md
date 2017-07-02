#2017年7月1日
##
##任务日志

###Markdown
* 了解markdown的用途
* 下载并破解MarkdownPad 2   ( 遇到了在win10下html渲染错误的问题，解决办法：下载 awesomium_v1.6.6_sdk_win 后重启电脑 ）
* 学习markdown语法  
##
###Git与GitHub
* git为版本控制工具
* github是一个用git做版本控制的项目托管**平台**
##
###GitHub
######基本操作
1. 创建存储库
* 在客户端克隆刚刚在浏览器中创建的库
* 右键点击库名---open in explorer---编辑文本-客户端changes---蓝色为提交的change---点击变为绿色为不提交的change
* 上传与同步到网页---右上角Sync
######分支
1. 创建分支
2. 提交到新分支---实线圈：当前的节点 虚线圈：下一次修改的节点
3. 切换分支（提交完当前分支才能切换）---上传/同步分支---删除分支（本地/网页）
4. 合并分支：update from...
######GitHub Flow
* 为团队成员写入权限：settings---collaborators.  ---添加新分支---添加提交---团队成员共享
######Pull Request
* 对别人仓库中的内容进行更改：fork---clone---pull request---原仓库review---是否merge
##
###Git
######创建账户
* $ git config --global user.name "Your Name"
* $ git config --global user.email "email@example.com"
######创建仓库
* 初始化git仓库： git init命令
2. 把文件添加到仓库： git add 文件名称.txt  （把文件修改添加到暂存区）
3. 把文件提交到仓库： git commit -m"本次提交的说明"  （把暂存区的所有内容提交到当前分支）
4. 掌握工作区的状态：git status
5. 查看修改内容：git diff(工作区和暂存区的比较）
6. 查看仓库文本内容：cat 文件名称.txt
######版本回退
* 查看修改历史：git log 
* 简洁的查看修改历史：git log --pretty=oneline 
3. HEAD:当前版本---HEAD^：上个版本---HEAD^^：上上个版本---前n个版本：HEAD~n
4. 回退版本：git reset --hard HEAD^
5. 找回版本：git reset --hard 版本号前几位
6. 记录每一次命令：git reflog
######版本库
![]()![](http://i.imgur.com/d6x9BrL.jpg)
![](http://i.imgur.com/uegK5G3.jpg)
![](http://i.imgur.com/ioj2n5A.jpg)
######撤销修改
![](http://i.imgur.com/gVVaTru.png)
######删除文件
* 直接删除工作区文件：rm file
* 删除工作区和暂存区文件：git rm file




