#include <stdio.h>
#include "calc3.h"
#include "y.tab.h"

static int lbl;

inline char to_variable_name(idNodeType* id_node) {
    return (char)(id_node->i + 'a');
}

int ex(nodeType* p) {

    int lbl1, lbl2;

    if (!p)
        return 0;

    switch (p->type) {

    case typeCon:
        printf("\tpushq\t$%d\n", p->con.value);
        break;

    case typeId:
        printf("\tleaq\t%c(%%rip), %%rsi\n", to_variable_name(&p->id));
        printf("\tmovq\t(%%rsi), %%rax\n");
        printf("\tpushq\t%%rax\n");
        break;

    case typeOpr:
        switch (p->opr.oper) {

        case WHILE:
            printf("L%03d:\n", lbl1 = lbl++);
            ex(p->opr.op[0]);

            printf("\tpopq\t%%rax\n");
            printf("\ttestq\t%%rax, %%rax\n");
            printf("\tjz\tL%03d\n", lbl2 = lbl++);
            printf("\tpushq\t%%rax\n");

            ex(p->opr.op[1]);
            printf("\tjmp\tL%03d\n", lbl1);
            printf("L%03d:\n", lbl2);
            break;

        case IF:
            ex(p->opr.op[0]);
            printf("\tpopq\t%%rax\n");
            printf("\ttestq\t%%rax, %%rax\n");

            if (p->opr.nops < 3) {
                /* if */
                printf("\tjz\tL%03d\n", lbl1 = lbl++);
                ex(p->opr.op[1]);
                printf("L%03d:\n", lbl1);
            } else {
                /* if else */
                printf("\tjz\tL%03d\n", lbl1 = lbl++);
                ex(p->opr.op[1]);
                printf("\tjmp\tL%03d\n", lbl2 = lbl++);
                printf("L%03d:\n", lbl1);
                ex(p->opr.op[2]);
                printf("L%03d:\n", lbl2);
            }
            break;

        case PRINT:
            ex(p->opr.op[0]);
            printf("\tpopq\t%%rdi\n");
            printf("\tcall\tprint\n");
            break;

        case '=':
            ex(p->opr.op[1]);
            printf("\tpopq\t%%rax\n");
            printf("\tleaq\t%c(%%rip), %%rsi\n",
                   to_variable_name(&p->opr.op[0]->id));
            printf("\tmovq\t%%rax, (%%rsi)\n");
            break;

        case UMINUS:
            ex(p->opr.op[0]);
            printf("\tpopq\t%%rax\n");
            printf("\tnegq\t%%rax\n");
            printf("\tpushq\t%%rax\n");
            break;

        case FACT:
            ex(p->opr.op[0]);
            printf("\tpopq\t%%rdi\n");
            printf("\tcvtsi2sdq\t%%rdi, %%xmm0\n");
            printf("\tcall\tfact\n");
            printf("\tpushq\t%%rax\n");
            break;

        case LNTWO:
            ex(p->opr.op[0]);
            printf("\tpopq\t%%rdi\n");
            printf("\tcvtsi2sdq\t%%rdi, %%xmm0\n");
            printf("\tcall\tlntwo\n");
            printf("\tcvttsd2siq\t%%xmm0, %%rax\n");
            printf("\tpushq\t%%rax\n");
            break;

        default:
            ex(p->opr.op[0]);
            ex(p->opr.op[1]);

            printf("\tpopq\t%%rbx\n");
            printf("\tpopq\t%%rax\n");
            switch (p->opr.oper) {

            case GCD:
                // printf("\tmovq\t%%rax, %%rdi\n");
                // printf("\tmovq\t%%rbx, %%rsi\n");
                printf("\tcvtsi2sdq\t%%rax, %%xmm0\n");
                printf("\tcvtsi2sdq\t%%rbx, %%xmm1\n");
                printf("\tcall\tgcd\n");
                printf("\tcvttsd2siq\t%%xmm0, %%rax\n");
                break;

            case '+':
                printf("\taddq\t%%rbx, %%rax\n");
                break;

            case '-':
                printf("\tsubq\t%%rbx, %%rax\n");
                break;

            case '*':
                printf("\timulq\t%%rbx, %%rax\n");
                break;

            case '/':
                printf("\txorq\t%%rdx, %%rdx\n");
                printf("\tmovq\t%%rbx, %%rcx\n");
                printf("\tdiv\t%%rcx\n");
                break;

            case '<':
                printf("\tcmp\t%%rbx, %%rax\n");
                printf("\tsetl\t%%al\n");
                printf("\tmovzbq\t%%al, %%rax\n");
                break;

            case '>':
                printf("\tcmp\t%%rbx, %%rax\n");
                printf("\tsetg\t%%al\n");
                printf("\tmovzbq\t%%al, %%rax\n");
                break;

            case LE:
                printf("\tcmp\t%%rbx, %%rax\n");
                printf("\tsetle\t%%al\n");
                printf("\tmovzbq\t%%al, %%rax\n");
                break;

            case GE:
                printf("\tcmp\t%%rbx, %%rax\n");
                printf("\tsetge\t%%al\n");
                printf("\tmovzbq\t%%al, %%rax\n");
                break;

            case NE:
                printf("\tcmp\t%%rbx, %%rax\n");
                printf("\tsetne\t%%al\n");
                printf("\tmovzbq\t%%al, %%rax\n");
                break;

            case EQ:
                printf("\tcmp\t%%rbx, %%rax\n");
                printf("\tsete\t%%al\n");
                printf("\tmovzbq\t%%al, %%rax\n");
                break;

            default:
                printf("\tpushq\t%%rax\n");
                printf("\tmovq\t%%rbx, %%rax\n");
                break;
            }
            printf("\tpushq\t%%rax\n");
        }
    }

    return 0;
}
