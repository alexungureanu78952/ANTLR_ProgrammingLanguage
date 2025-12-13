// Generated from c:/Users/straj/Documents/Facultate/LFC/LFC_Tema_2/ANTLR_ProgrammingLanguage/MyPL/MyPL.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link MyPLParser}.
 */
public interface MyPLListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link MyPLParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(MyPLParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(MyPLParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#globalDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterGlobalDeclaration(MyPLParser.GlobalDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#globalDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitGlobalDeclaration(MyPLParser.GlobalDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#variableDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterVariableDeclaration(MyPLParser.VariableDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#variableDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitVariableDeclaration(MyPLParser.VariableDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterFunctionDeclaration(MyPLParser.FunctionDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitFunctionDeclaration(MyPLParser.FunctionDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#parameterList}.
	 * @param ctx the parse tree
	 */
	void enterParameterList(MyPLParser.ParameterListContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#parameterList}.
	 * @param ctx the parse tree
	 */
	void exitParameterList(MyPLParser.ParameterListContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#parameter}.
	 * @param ctx the parse tree
	 */
	void enterParameter(MyPLParser.ParameterContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#parameter}.
	 * @param ctx the parse tree
	 */
	void exitParameter(MyPLParser.ParameterContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#block}.
	 * @param ctx the parse tree
	 */
	void enterBlock(MyPLParser.BlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#block}.
	 * @param ctx the parse tree
	 */
	void exitBlock(MyPLParser.BlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterStatement(MyPLParser.StatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitStatement(MyPLParser.StatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#assignmentStatement}.
	 * @param ctx the parse tree
	 */
	void enterAssignmentStatement(MyPLParser.AssignmentStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#assignmentStatement}.
	 * @param ctx the parse tree
	 */
	void exitAssignmentStatement(MyPLParser.AssignmentStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#assignmentOperator}.
	 * @param ctx the parse tree
	 */
	void enterAssignmentOperator(MyPLParser.AssignmentOperatorContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#assignmentOperator}.
	 * @param ctx the parse tree
	 */
	void exitAssignmentOperator(MyPLParser.AssignmentOperatorContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void enterIfStatement(MyPLParser.IfStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void exitIfStatement(MyPLParser.IfStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void enterForStatement(MyPLParser.ForStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void exitForStatement(MyPLParser.ForStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#forInit}.
	 * @param ctx the parse tree
	 */
	void enterForInit(MyPLParser.ForInitContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#forInit}.
	 * @param ctx the parse tree
	 */
	void exitForInit(MyPLParser.ForInitContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#forUpdate}.
	 * @param ctx the parse tree
	 */
	void enterForUpdate(MyPLParser.ForUpdateContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#forUpdate}.
	 * @param ctx the parse tree
	 */
	void exitForUpdate(MyPLParser.ForUpdateContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void enterWhileStatement(MyPLParser.WhileStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void exitWhileStatement(MyPLParser.WhileStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#returnStatement}.
	 * @param ctx the parse tree
	 */
	void enterReturnStatement(MyPLParser.ReturnStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#returnStatement}.
	 * @param ctx the parse tree
	 */
	void exitReturnStatement(MyPLParser.ReturnStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#expressionStatement}.
	 * @param ctx the parse tree
	 */
	void enterExpressionStatement(MyPLParser.ExpressionStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#expressionStatement}.
	 * @param ctx the parse tree
	 */
	void exitExpressionStatement(MyPLParser.ExpressionStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code MultDivModExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterMultDivModExpr(MyPLParser.MultDivModExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code MultDivModExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitMultDivModExpr(MyPLParser.MultDivModExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code RelationalExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterRelationalExpr(MyPLParser.RelationalExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code RelationalExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitRelationalExpr(MyPLParser.RelationalExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code UnaryExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterUnaryExpr(MyPLParser.UnaryExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code UnaryExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitUnaryExpr(MyPLParser.UnaryExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LogicalAndExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterLogicalAndExpr(MyPLParser.LogicalAndExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LogicalAndExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitLogicalAndExpr(MyPLParser.LogicalAndExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PrefixExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterPrefixExpr(MyPLParser.PrefixExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PrefixExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitPrefixExpr(MyPLParser.PrefixExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PostfixExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterPostfixExpr(MyPLParser.PostfixExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PostfixExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitPostfixExpr(MyPLParser.PostfixExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LogicalOrExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterLogicalOrExpr(MyPLParser.LogicalOrExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LogicalOrExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitLogicalOrExpr(MyPLParser.LogicalOrExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code FunctionCallExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCallExpr(MyPLParser.FunctionCallExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code FunctionCallExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCallExpr(MyPLParser.FunctionCallExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code EqualityExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterEqualityExpr(MyPLParser.EqualityExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code EqualityExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitEqualityExpr(MyPLParser.EqualityExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdentifierExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterIdentifierExpr(MyPLParser.IdentifierExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdentifierExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitIdentifierExpr(MyPLParser.IdentifierExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LiteralExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterLiteralExpr(MyPLParser.LiteralExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LiteralExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitLiteralExpr(MyPLParser.LiteralExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ParenExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterParenExpr(MyPLParser.ParenExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ParenExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitParenExpr(MyPLParser.ParenExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AddSubExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterAddSubExpr(MyPLParser.AddSubExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AddSubExpr}
	 * labeled alternative in {@link MyPLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitAddSubExpr(MyPLParser.AddSubExprContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#argumentList}.
	 * @param ctx the parse tree
	 */
	void enterArgumentList(MyPLParser.ArgumentListContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#argumentList}.
	 * @param ctx the parse tree
	 */
	void exitArgumentList(MyPLParser.ArgumentListContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#literal}.
	 * @param ctx the parse tree
	 */
	void enterLiteral(MyPLParser.LiteralContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#literal}.
	 * @param ctx the parse tree
	 */
	void exitLiteral(MyPLParser.LiteralContext ctx);
	/**
	 * Enter a parse tree produced by {@link MyPLParser#type}.
	 * @param ctx the parse tree
	 */
	void enterType(MyPLParser.TypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link MyPLParser#type}.
	 * @param ctx the parse tree
	 */
	void exitType(MyPLParser.TypeContext ctx);
}