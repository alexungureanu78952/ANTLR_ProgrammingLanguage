grammar MyPL;

// Parser rules
program: statement* EOF;

statement: expression ';';

expression: ID;

// Lexer rules
ID: [a-zA-Z_][a-zA-Z0-9_]*;
WS: [ \t\r\n]+ -> skip;