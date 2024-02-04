#/bin/bash

commits=("c6b7222" "220e190" "0b4bbc9" "e5ffbb0" "eecfb05" "5825e75" "462e0b8" "898d385" "0303f09" "8317adb" "270b5a2" "1b742a0" "ff0b403")
length=${#commits[@]}
names=("0_Baseline" "1-1_O1" "1-2_O2" "1-3_O3" "2-1_Extracting_Gaussian_Filter_Allocation" "2-2_Caching_Loop_End" "2-3_Caching_Image_Lines" "2-4_Avoid_branch_checking_for_bounds" 
"3-1_Basic_Optimizations" "3-2_Replace_read_by_get" "3-3_Write_after_interleaving" "3-4_Read_before_De-Interleaving" "4-1_Exploiting_Monochromacy")

for ((i = 0; i < length; i++)); do
	commit=${commits[i]}
	name=${names[i]}
	if [ ! -d ../$name ]; then
		git worktree add ../$name $commit
	fi
done