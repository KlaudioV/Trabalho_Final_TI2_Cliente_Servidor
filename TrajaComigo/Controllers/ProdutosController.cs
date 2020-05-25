﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrajaComigo.Data;
using TrajaComigo.Models;

namespace TrajaComigo.Controllers
{
    public class ProdutosController : Controller
    {
        /// este atributo representa uma referência à nossa base de dados
        private readonly TrajaComigoBD db;

        /// atributo que recolhe nele os dados do Servidor
        private readonly IWebHostEnvironment _caminho;

        public ProdutosController(TrajaComigoBD context, IWebHostEnvironment caminho)
        {
            this.db = context;
            this._caminho = caminho;
        }
        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {

            // LINQ
            // db.Veterinarios.ToListAsync()  <=>    SELECT * FROM Veterinarios;

            return View(await db.Produtos.ToListAsync());
        }


        // GET: Veterinarios
        public async Task<IActionResult> Index2()
        {

            // LINQ
            // db.Veterinarios.ToListAsync()  <=>    SELECT * FROM Veterinarios;

            return View(await db.Produtos.ToListAsync());
        }


        // GET: Veterinarios/Details/5
        /// <summary>
        /// Mostra os detalhes de um Veterinário, usando Lazy Loading
        /// </summary>
        /// <param name="id">valor da PK do veterinário. Admite um valor Null, por causa do sinal ? </param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                // se o ID é null, é porque o meu utilizador está a testar a minha aplicação
                // redireciono para o método INDEX deste mesmo controller
                return RedirectToAction("Index");
            }

            // esta expressão db.Veterinarios.FirstOrDefaultAsync(m => m.ID == id)
            // é uma forma diferente de escrever o seguinte comando
            // SELECT * FROM db.Veterinarios v WHERE v.ID = id
            // esta expressão é escrita em LINQ
            var veterinario = await db.Produtos.FirstOrDefaultAsync(v => v.Id == id);

            if (veterinario == null)
            {
                // se o ID é null, é porque o meu utilizador está a testar a minha aplicação
                // ele introduziu manualmente um valor inexistente
                // redireciono para o método INDEX deste mesmo controller
                return RedirectToAction("Index");
            }

            return View(veterinario);
        }



        // GET: Veterinarios/Details/5
        /// <summary>
        /// Mostra os detalhes de um Veterinário, usando Eager Loading
        /// </summary>
        /// <param name="id">valor da PK do veterinário. Admite um valor Null, por causa do sinal ? </param>
        /// <returns></returns>
        public async Task<IActionResult> Details2(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // é uma forma diferente de escrever o seguinte comando
            /// SELECT * 
            /// FROM db.Veterinarios v, db.Consultas c, db.Animais a, db.Donos d
            /// WHERE c.VeterinarioFK=v.ID AND
            ///       c.AnimalFK=a.ID AND
            ///       a.AnimalFK=d.ID AND
            ///       v.ID = id

            // esta expressão é escrita em LINQ
            var produto = await db.Produtos
                                      .FirstOrDefaultAsync(v => v.Id == id);


            var vvv = from v in db.Produtos
                      select v;



            if (produto == null)
            {
                return RedirectToAction("Index");
            }

            return View(produto);
        }



        // GET: Veterinarios/Create
        /// <summary>
        /// invocar a View de criação de um novo Veterinário
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }




        // POST: Veterinarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Designacao,Descricao,Imagem")] Produtos Produto, IFormFile ProdutoImg)
        {

            //***************************************
            // processar a imagem
            //***************************************

            // vars. auxiliares
            bool haFicheiro = false;
            string caminhoCompleto = "";

            // será q há imagem?
            if (ProdutoImg == null)
            {
                // o utilizador não fez upload de um ficheiro
                Produto.Imagem = "avatar.png";
            }
            else
            {
                // existe fotografia.
                // Mas, será boa?
                if (ProdutoImg.ContentType == "image/jpeg" ||
                    ProdutoImg.ContentType == "image/png")
                {
                    // estamos perante uma boa foto
                    // temos de gerar um nome para o ficheiro
                    Guid g;
                    g = Guid.NewGuid();
                    // obter a extensão do ficheiro
                    string extensao = Path.GetExtension(ProdutoImg.FileName).ToLower();
                    string nomeFicheiro = g.ToString() + extensao;
                    // onde guardar o ficheiro
                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens\\produtos", nomeFicheiro);
                    // atribuir o nome do ficheiro ao Veterinário
                    Produto.Imagem = nomeFicheiro;
                    // marcar q existe uma fotografia
                    haFicheiro = true;
                }
                else
                {
                    // o ficheiro não é válido
                    Produto.Imagem = "avatar.png";
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // adiciona o novo veterinário à BD, mas na memória do servidor ASP .NET
                    db.Add(Produto);
                    // consolida os dados no Servidor BD (commit)
                    await db.SaveChangesAsync();
                    // será q há foto para gravar?
                    if (haFicheiro)
                    {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await ProdutoImg.CopyToAsync(stream);
                    }
                    // redireciona a ação para a View do Index
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                throw;
            }

            // qd ocorre um erro, reenvio os dados do veterinário para a view da criação
            return View(Produto);
        }





        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                // se o ID é null, é porque o meu utilizador está a testar a minha aplicação
                // redireciono para o método INDEX deste mesmo controller
                return RedirectToAction("Index");
            }

            // esta expressão db.Veterinarios.FindAsync(id)
            // é uma forma diferente de escrever o seguinte comando
            // SELECT * FROM db.Veterinarios v WHERE v.ID = id
            // esta expressão é escrita em LINQ
            var veterinario = await db.Produtos.FindAsync(id);

            if (veterinario == null)
            {
                // se o ID é null, é porque o meu utilizador está a testar a minha aplicação
                // ele introduziu manualmente um valor inexistente
                // redireciono para o método INDEX deste mesmo controller
                return RedirectToAction("Index");
            }
            return View(veterinario);
        }





        // POST: Veterinarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Designacao,Descricao,Imagem")] Produtos produtos)
        {
            if (id != produtos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(produtos);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinariosExists(produtos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produtos);
        }





        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                // se o ID é null, é porque o meu utilizador está a testar a minha aplicação
                // redireciono para o método INDEX deste mesmo controller
                return RedirectToAction("Index");
            }

            // esta expressão db.Veterinarios.FirstOrDefaultAsync(m => m.ID == id)
            // é uma forma diferente de escrever o seguinte comando
            // SELECT * FROM db.Veterinarios v WHERE v.ID = id
            // esta expressão é escrita em LINQ
            var veterinario = await db.Produtos.FirstOrDefaultAsync(v => v.Id == id);

            if (veterinario == null)
            {
                // se o ID é null, é porque o meu utilizador está a testar a minha aplicação
                // ele introduziu manualmente um valor inexistente
                // redireciono para o método INDEX deste mesmo controller
                return RedirectToAction("Index");
            }

            return View(veterinario);
        }




        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinarios = await db.Produtos.FindAsync(id);
            db.Produtos.Remove(veterinarios);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool VeterinariosExists(int id)
        {
            return db.Produtos.Any(e => e.Id == id);
        }



    }
}