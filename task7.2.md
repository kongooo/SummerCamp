#7月2日日志  
##  
##git  
###远程仓库  
####连接github与git  
1. Git Bash---$ ssh-keygen -t rsa -C "youremail@example.com"---.ssh目录
2. 从本地电脑的git仓库向github推送：在github上添加ssh key  
####从本地库向远程库推送  
1. 新建仓库后（不勾选），根据github提示，把本地库的内容推送到远程
2. 关联远程库命令：git remote add origin https://github.com/kongooo(账户名称)/库名.git
3. 第一次推送master分支命令：git push -u origin master
4. 本地提交后向github推送最新修改命令：git push origin 分支name  
####从远程库向本地库克隆  
* git clone https://github.com/kongooo(账户名称）/库名.git  
####分支管理  
######创建与合并分支  
* 查看分支：git branch
* 创建分支：git branch name
* 切换分支：git checkout name
* 创建+切换分支：git checkout -b name
* 合并某分支到当前分支：git merge name
* 删除分支：git branch -d name  
######解决冲突  
* 当git无法自动合并分支时，首先解决冲突，再提交，合并完成
* 查看分支合并图：git log --graph --pretty=oneline --abbrev-commit  
######分支管理策略  
* 禁用 Fast forward 合并分支：git merge --no-ff -m "说明文字" name
* Fast forward merge:看不出曾经做过合并  
######bug分支  
* 储存当前分支内容：git stash
* 查看stash中存储情况：git stash list
* 恢复但不删除stash中内容：git stash apply
* 删除stash中内容：git stash drop
* 恢复同时删除stash中内容:git stash pop
* 强制删除未合并分支：git branch -D name  
######多人协作  
* 查看远程库信息：git remote -v
* 建立本地分支与远程分支的关联：git branch --set-upstream branch-name origin/branch-name(本地分支与远程分支名称最好一致)
* push失败---git pull(抓取远程的新提交)  
####标签管理  
######创建标签  
* git tag name commit id(不指定id默认打在HEAD上）
* git tag -a tagname -m "标签信息"
* 查看所有标签：git tag
* 查看标签信息：git show tagname  
######操作标签  
* 删除本地标签：git tag -d tagname
* 推送本地标签：git push origin tagname
* 推送全部未推送过的本地标签：git push origin --tags
* 删除一个远程标签：git push origin :refs/tags/tagname  
####配置别名  
* git config --global alias.myname oldname
* 查看配置文件：cat. git/config
* 用户配置文件：cat. gitconfig(在主目录下）  
##  
##总结  
* 学习流程：与学习笔记相同：markdown---github---git（先git再github会快一些），边看教程边实践然后做笔记。
* 难点：git分支  
