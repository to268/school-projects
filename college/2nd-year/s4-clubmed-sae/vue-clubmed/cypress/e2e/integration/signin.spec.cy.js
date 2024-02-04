describe('Test du formulaire de création de compte', () => {
  beforeEach(() => {
    cy.visit('http://localhost:5173/signin')
  })

  it('Remplir et soumettre le formulaire', () => {
    cy.get('#nom').type('Doe')
    cy.get('#prenom').type('John')
    cy.get('#email').type('testdeezmailnuts@example.com')
    cy.get('#telephone').type('0102030102')
    cy.get('#password').type('azertyuiop')
    cy.get('#repeatpassword').type('azertyuiop')
    cy.get('#cgu').check()
    cy.get('#politique').check()
    cy.get('.buttcreer').click()
  })
})