#/bin/bash

commits=("")
length=${#commits[@]}
names=("")

for ((i = 0; i < length; i++)); do
	commit=${commits[i]}
	name=${names[i]}
	if [ ! -d ../$name ]; then
		git worktree add ../$name $commit
	fi
done
