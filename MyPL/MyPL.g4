grammar MyPL;

// PARSER RULES

// Program entry point
program: (globalDeclaration | functionDeclaration)* EOF;

// Global declarations
globalDeclaration: variableDeclaration;

// Variable declaration
variableDeclaration:
	CONST? type ID (ASSIGN expression)? SEMICOLON
	| CONST? type ID ASSIGN expression (
		COMMA ID ASSIGN expression
	)* SEMICOLON;

// Function declaration
functionDeclaration:
	type ID LPAREN parameterList? RPAREN block
	| VOID ID LPAREN parameterList? RPAREN block;

// Parameter list
parameterList: parameter (COMMA parameter)*;

parameter: type ID;

// Block
block: LBRACE statement* RBRACE;

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
assignmentStatement: ID assignmentOperator expression SEMICOLON;

assignmentOperator:
	ASSIGN
	| PLUS_ASSIGN
	| MINUS_ASSIGN
	| MULT_ASSIGN
	| DIV_ASSIGN
	| MOD_ASSIGN;

// If statement
ifStatement:
	IF LPAREN expression RPAREN statement (ELSE statement)?;

// For statement
forStatement:
	FOR LPAREN forInit? SEMICOLON expression? SEMICOLON forUpdate? RPAREN statement;

forInit: variableDeclaration | assignmentStatement | expression;

forUpdate: assignmentStatement | expression;

// While statement
whileStatement: WHILE LPAREN expression RPAREN statement;

// Return statement
returnStatement: RETURN expression? SEMICOLON;

// Expression statement
expressionStatement: expression SEMICOLON;

// Expressions with precedence
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

// Argument list
argumentList: expression (COMMA expression)*;

// Literal
literal: INT_LITERAL | FLOAT_LITERAL | STRING_LITERAL;

// Types
type: INT | FLOAT | DOUBLE | STRING;

// LEXER RULES

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

// Operators - Assignment
ASSIGN: '=';
PLUS_ASSIGN: '+=';
MINUS_ASSIGN: '-=';
MULT_ASSIGN: '*=';
DIV_ASSIGN: '/=';
MOD_ASSIGN: '%=';

// Operators - Arithmetic
PLUS: '+';
MINUS: '-';
MULT: '*';
DIV: '/';
MOD: '%';
INCREMENT: '++';
DECREMENT: '--';

// Operators - Relational
LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '==';
NE: '!=';

// Operators - Logical
AND: '&&';
OR: '||';
NOT: '!';

// Delimiters
LPAREN: '(';
RPAREN: ')';
LBRACE: '{';
RBRACE: '}';
SEMICOLON: ';';
COMMA: ',';

// Literals
INT_LITERAL: [0-9]+;

FLOAT_LITERAL: [0-9]+ '.' [0-9]+;

STRING_LITERAL: '"' (~["\r\n\\] | '\\' .)* '"';

// Identifiers
ID: [a-zA-Z_][a-zA-Z0-9_]*;

// Comments (ignored)
LINE_COMMENT: '//' ~[\r\n]* -> skip;

BLOCK_COMMENT: '/*' .*? '*/' -> skip;

// Whitespace (ignored)
WS: [ \t\r\n]+ -> skip;