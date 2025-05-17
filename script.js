class Calculadora {
  somar(a, b) {
    return a + b;
  }

  subtrair(a, b) {
    return a - b;
  }

  multiplicar(a, b) {
    return a * b;
  }

  dividir(a, b) {
    return a / b;
  }
}

const calculo = new Calculadora();
  let resultadoCalculo;

    if (tipoCalculo == "somar") {
    resultado = calculo.somar(valor1, valor2);
    } 
    else if (tipo == "subtrair") {
    resultado = calculo.subtrair(valor1, valor2);
    } 
    else if (tipo == "multiplicar") {
    resultado = calculo.multiplicar(valor1, valor2);
    } 
    else if (tipo == "dividir") {
    resultado = calculo.dividir(valor1, valor2);
    }

//function chamarCalculo() {
//
//    let valor1 = document.getElementById('numValor1').value
//    let valor2 = document.getElementById("numValor2").value
//    let operacao = document.getElementById('selectOperacao').value
//
//    let resultadoOperacao = eval(parseInt(valor1) + operacao + parseInt(valor2))
//
//    let resultado = document.getElementById('numResultado')
//
//    resultado.value = resultadoOperacao
//}
