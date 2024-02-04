all: filters pearson

filters:
	$(MAKE) -C $@

pearson:
	$(MAKE) -C $@

verify: filters pearson
	cd filters && bash verify.sh ./blur_par ./threshold_par
	cd pearson && bash verify.sh

clean:
	$(MAKE) -C filters clean
	$(MAKE) -C pearson clean

mrproper:
	$(MAKE) -C filters mprproper
	$(MAKE) -C pearson mprproper

.PHONY: filters pearson verify clean mrproper
