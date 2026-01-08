grammar MyPL;

// PARSER RULES

program: (globalDeclaration | functionDeclaration)* EOF;

globalDeclaration: variableDeclaration;

variableDeclaration:
	CONST? type ID (ASSIGN expression)? SEMICOLON
	| CONST? type ID ASSIGN expression (
		COMMA ID ASSIGN expression
	)* SEMICOLON;

functionDeclaration:
	type ID LPAREN parameterList? RPAREN block
	| VOID ID LPAREN parameterList? RPAREN block;

parameterList: parameter (COMMA parameter)*;

parameter: type ID;

block: LBRACE statement* RBRACE;

statement:
	variableDeclaration
	| assignmentStatement
	| ifStatement
	| forStatement
	| whileStatement
	| returnStatement
	| expressionStatement
	| block;

assignmentStatement: ID assignmentOperator expression SEMICOLON;

assignmentOperator:
	ASSIGN
	| PLUS_ASSIGN
	| MINUS_ASSIGN
	| MULT_ASSIGN
	| DIV_ASSIGN
	| MOD_ASSIGN;

ifStatement:
	IF LPAREN expression RPAREN statement (ELSE statement)?;

forStatement:
	FOR LPAREN forInit? SEMICOLON expression? SEMICOLON forUpdate? RPAREN statement;

forInit: variableDeclaration | assignmentStatement | expression;

forUpdate: assignmentStatement | expression;

whileStatement: WHILE LPAREN expression RPAREN statement;

returnStatement: RETURN expression? SEMICOLON;

expressionStatement: expression SEMICOLON;

expression:
	literal										# LiteralExpr
	| ID										# IdentifierExpr
	| ID LPAREN argumentList? RPAREN			# FunctionCallExpr
	| LPAREN expression RPAREN					# ParenExpr
	| (INCREMENT | DECREMENT) expression		# PrefixExpr
	| expression (INCREMENT | DECREMENT)		# PostfixExpr
	| (NOT | MINUS | PLUS) expression			# UnaryExpr
	| expression (MULT | DIV | MOD) expression	# MultDivModExpr
	| expression (PLUS | MINUS) expression		# AddSubExpr
	| expression (LT | GT | LE | GE) expression	# RelationalExpr
	| expression (EQ | NE) expression			# EqualityExpr
	| expression AND expression					# LogicalAndExpr
	| expression OR expression					# LogicalOrExpr;

argumentList: expression (COMMA expression)*;

literal: INT_LITERAL | FLOAT_LITERAL | STRING_LITERAL;

type: INT | FLOAT | DOUBLE | STRING;

// LEXER RULES

CONST: 'const';
INT: 'int';
FLOAT: 'float';
DOUBLE: 'double';
STRING: 'string';
VOID: 'void';
IF: 'if';
ELSE: 'else';
FOR: 'for';
WHILE: 'while';
RETURN: 'return';

ASSIGN: '=';
PLUS_ASSIGN: '+=';
MINUS_ASSIGN: '-=';
MULT_ASSIGN: '*=';
DIV_ASSIGN: '/=';
MOD_ASSIGN: '%=';

PLUS: '+';
MINUS: '-';
MULT: '*';
DIV: '/';
MOD: '%';
INCREMENT: '++';
DECREMENT: '--';

LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '==';
NE: '!=';

AND: '&&';
OR: '||';
NOT: '!';

LPAREN: '(';
RPAREN: ')';
LBRACE: '{';
RBRACE: '}';
SEMICOLON: ';';
COMMA: ',';

INT_LITERAL: [0-9]+;

FLOAT_LITERAL: [0-9]+ '.' [0-9]+;

STRING_LITERAL: '"' (~["\r\n\\] | '\\' .)* '"';

ID: [a-zA-Z_][a-zA-Z0-9_]*;

LINE_COMMENT: '//' ~[\r\n]* -> skip;

BLOCK_COMMENT: '/*' .*? '*/' -> skip;

WS: [ \t\r\n]+ -> skip;