grammar MyPL;

// Parser rules
program: (globalDeclaration | functionDeclaration)* EOF;

// Global declarations
globalDeclaration: variableDeclaration;

// Variable declaration
variableDeclaration:
	CONST? type ID ('=' expression)? ';'
	| CONST? type ID '=' expression (',' ID '=' expression)* ';';

// Function declaration
functionDeclaration:
	type ID '(' parameterList? ')' block
	| VOID ID '(' parameterList? ')' block;

// Parameter list
parameterList: parameter (',' parameter)*;

parameter: type ID;

// Block
block: '{' statement* '}';

// Statements
statement:
	variableDeclaration
	| assignmentStatement
	| ifStatement
	| forStatement
	| whileStatement
	| returnStatement
	| expressionStatement
	| block;

// Assignment statement
assignmentStatement: ID assignmentOperator expression ';';

assignmentOperator: '=' | '+=' | '-=' | '*=' | '/=' | '%=';

// If statement
ifStatement: IF '(' expression ')' statement (ELSE statement)?;

// For statement
forStatement:
	FOR '(' forInit? ';' expression? ';' forUpdate? ')' statement;

forInit: variableDeclaration | assignmentStatement | expression;

forUpdate: assignmentStatement | expression;

// While statement
whileStatement: WHILE '(' expression ')' statement;

// Return statement
returnStatement: RETURN expression? ';';

// Expression statement
expressionStatement: expression ';';

// Expressions
expression:
	literal												# LiteralExpr
	| ID												# IdentifierExpr
	| ID '(' argumentList? ')'							# FunctionCallExpr
	| '(' expression ')'								# ParenExpr
	| ('++' | '--') expression							# PrefixExpr
	| expression ('++' | '--')							# PostfixExpr
	| ('!' | '-' | '+') expression						# UnaryExpr
	| expression ('*' | '/' | '%') expression			# MultDivModExpr
	| expression ('+' | '-') expression					# AddSubExpr
	| expression ('<' | '>' | '<=' | '>=') expression	# RelationalExpr
	| expression ('==' | '!=') expression				# EqualityExpr
	| expression '&&' expression						# LogicalAndExpr
	| expression '||' expression						# LogicalOrExpr;

// Argument list
argumentList: expression (',' expression)*;

// Literal
literal: INT_LITERAL | FLOAT_LITERAL | STRING_LITERAL;

// Types
type: INT | FLOAT | DOUBLE | STRING;

// Keywords
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

// Literals
INT_LITERAL: [0-9]+;
FLOAT_LITERAL: [0-9]+ '.' [0-9]+;
STRING_LITERAL: '"' (~["\r\n\\] | '\\' .)* '"';

// Identifiers
ID: [a-zA-Z_][a-zA-Z0-9_]*;

// Comments
LINE_COMMENT: '//' ~[\r\n]* -> skip;
BLOCK_COMMENT: '/*' .*? '*/' -> skip;

// Whitespace
WS: [ \t\r\n]+ -> skip;