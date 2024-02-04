#include <stdio.h>
#include <stdint.h>
#include "calc3.h"
#include "y.tab.h"

static uint64_t stack[1000];
static uint64_t stackPointer = 0;
static int lbl = 0;

inline char to_variable_name(idNodeType* id_node) {
    return (char)(id_node->i + 'a');
}

int ex(nodeType* p) {
    int lbl1, lbl2;

    if (!p)
        return 0;

    switch (p->type) {
    case typeCon:
        printf("\tpush(%d);\n", p->con.value);
        break;

    case typeId:
        printf("\tpush(%c);\n", p->id.i + 'a');
        break;

    case typeOpr:
        switch (p->opr.oper) {
        case WHILE:
            lbl1 = lbl++;
            lbl2 = lbl++;
            printf("L%d:\n", lbl1);
            ex(p->opr.op[0]);
            printf("\tif (!pop()) goto L%d;\n", lbl2);
            ex(p->opr.op[1]);
            printf("\tgoto L%d;\n", lbl1);
            printf("\tL%d:\n", lbl2);
            break;

        case IF:
            lbl1 = lbl++;
            lbl2 = lbl++;
            ex(p->opr.op[0]);
            printf("\tif (pop()) {\n");
            printf("\t\tgoto L%d;\n", lbl1);
            printf("\t} else {\n");
            ex(p->opr.op[2]);
            printf("\t\tgoto L%d;\n", lbl2);
            printf("\t}\n");
            printf("\t\tL%d:\n", lbl1);
            ex(p->opr.op[1]);
            printf("\t\tL%d:\n", lbl2);
            break;

        case PRINT:
            ex(p->opr.op[0]);
            printf("\tprintf(\"%%d\\n\", pop());\n");
            break;

        case '=':
            ex(p->opr.op[1]);

            printf("\t{\n");
            printf("\t\tint64_t tmp = pop();\n");
            printf("\t\t%c = tmp;\n", to_variable_name(&p->opr.op[0]->id));
            printf("\t}\n");
            break;

        case UMINUS:
            ex(p->opr.op[0]);
            printf("\tpush(-pop());\n");
            break;

        case FACT:
            ex(p->opr.op[0]);
            printf("\t{\n");
            printf("\t\tint64_t a = pop();\n");
            printf("\t\tpush(fact(a));\n");
            printf("\t}\n");
            break;

        case LNTWO:
            ex(p->opr.op[0]);
            printf("\t{\n");
            printf("\t\tpush(lntwo(pop()));\n");
            printf("\t\t}\n");
            break;

        default:
            ex(p->opr.op[0]);
            ex(p->opr.op[1]);
            printf("\t{\n");
            switch (p->opr.oper) {
            case GCD:
                printf("\t\tpush(gcd(pop(), pop()));\n");
                break;

            case '+':
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a + tmp_b);\n");
                break;

            case '-':
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a - tmp_b);\n");
                break;

            case '*':
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a * tmp_b);\n");
                break;

            case '/':
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a / tmp_b);\n");
                break;

            case '<':
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a < tmp_b ? 1 : 0);\n");
                break;

            case '>':
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a > tmp_b ? 1 : 0);\n");
                break;

            case LE:
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a <= tmp_b ? 1 : 0);\n");
                break;

            case GE:
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a >= tmp_b ? 1 : 0);\n");
                break;

            case EQ:
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a == tmp_b ? 1 : 0);\n");
                break;

            case NE:
                printf("\t\tint64_t tmp_b = pop();\n");
                printf("\t\tint64_t tmp_a = pop();\n");
                printf("\t\tpush(tmp_a != tmp_b ? 1 : 0);\n");
                break;
            }
            printf("\t}\n");
            break;
        }
        break;
    }

    return 0;
}
