#pragma once


// CColorButton

class CColorButton : public CButton
{
	DECLARE_DYNAMIC(CColorButton)

public:
	CColorButton();
	virtual ~CColorButton();

	void SetColor(COLORREF color) { m_color = color; Invalidate(); }
	COLORREF GetColor() { return m_color; }
	void PreSubclassWindow();
	void DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct);

protected:
	DECLARE_MESSAGE_MAP()
public:

private:
	COLORREF m_color;
};


