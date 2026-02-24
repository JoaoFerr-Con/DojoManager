import { useState } from 'react'
import axios from 'axios'
import { Shield, Lock, Mail } from 'lucide-react'

export default function Login({ onLogin }) {
  const [email, setEmail] = useState('admin@dojo.com') // Email padrão para teste
  const [senha, setSenha] = useState('123456')         // Senha padrão para teste
  const [erro, setErro] = useState('')

  const handleLogin = async (e) => {
    e.preventDefault()
    try {
      // Chama o endpoint de autenticação da sua API
      const response = await axios.post('http://localhost:5192/api/Auth/login', { email, senha })
      const { token } = response.data
      
      // Salva o Token no navegador para persistir o login
      localStorage.setItem('token', token)
      onLogin(token)
    } catch (err) {
      setErro('Acesso negado. Verifique suas credenciais de Mestre.')
    }
  }

  return (
    <div className="min-h-screen bg-slate-950 flex items-center justify-center p-4 font-sans">
      <div className="bg-white w-full max-w-md rounded-3xl shadow-2xl overflow-hidden animate-in zoom-in duration-500">
        <div className="bg-red-600 p-8 text-white text-center">
          <Shield size={48} className="mx-auto mb-4" />
          <h2 className="text-3xl font-black italic tracking-tighter">DOJO<span className="text-slate-900">MANAGER</span></h2>
          <p className="text-red-100 text-[10px] font-black uppercase tracking-widest mt-2">Área Administrativa • Unidade Belém</p>
        </div>
        
        <form onSubmit={handleLogin} className="p-8 space-y-6">
          {erro && (
            <div className="bg-red-50 text-red-600 p-3 rounded-xl text-xs font-bold border border-red-100 animate-bounce">
              {erro}
            </div>
          )}
          
          <div>
            <label className="text-[10px] font-black uppercase text-slate-400 ml-1">E-mail do Administrador</label>
            <div className="relative mt-1">
              <input type="email" required className="w-full pl-10 pr-4 py-3 bg-slate-50 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-red-500 transition-all" 
                     value={email} onChange={e => setEmail(e.target.value)} placeholder="mestre@dojo.com" />
              <Mail className="absolute left-3 top-3.5 text-slate-400" size={16} />
            </div>
          </div>

          <div>
            <label className="text-[10px] font-black uppercase text-slate-400 ml-1">Senha de Acesso</label>
            <div className="relative mt-1">
              <input type="password" required className="w-full pl-10 pr-4 py-3 bg-slate-50 border border-slate-200 rounded-xl outline-none focus:ring-2 focus:ring-red-500 transition-all" 
                     value={senha} onChange={e => setSenha(e.target.value)} placeholder="••••••" />
              <Lock className="absolute left-3 top-3.5 text-slate-400" size={16} />
            </div>
          </div>

          <button className="w-full bg-slate-950 text-white font-bold py-4 rounded-2xl hover:bg-black transition-all shadow-xl active:scale-95 flex items-center justify-center gap-2">
            ENTRAR NO TATAME
          </button>
        </form>
      </div>
    </div>
  )
}