require 'rake'
require 'date'
require 'net/http'
Dir.glob('**/*.rake').each { |r| import r}

desc 'Grab dependencies'
task :pillage do
  Net::HTTP.start("repo.sudocoders.net") do |http|
    resp = http.get("/objloader/ObjLoaderBleedingEdge.tar.gz")
    open("ObjLoaderBleedingEdge.tar.gz", "wb") { |file| file.write(resp.body) }
  end
  puts "Untarring dependencies."
  sh 'tar -xvf ObjLoaderBleedingEdge.tar.gz'
  sh 'cp ./source/CjClutter.ObjLoader.Loader/bin/Debug/*.dll .'
  sh 'rm -rf source'
  puts "Removing those tars."
  sh 'rm *.tar.gz'
end

desc 'Use dmcs to compile'
task :compile do
  sh "dmcs -r:OpenTK.dll -r:System.dll -r:System.Drawing.dll -r:CjClutter.ObjLoader.Loader.dll -out:Renderer.dll -target:library *.cs"
  sh "cp Renderer.dll ./TestRenderer/"
end

desc 'Test Renderer Locally'
task :test do
  Dir.chdir './TestRenderer' do  
    sh 'rake compile'
    sh 'rake test'
  end
end

desc 'Test Renderer on Jenkins'
task :testjenkins do
  Dir.chdir './TestRenderer' do
    sh 'rake compile'
    sh 'rake testjenkins'
  end
end
desc 'Package up the necessaries.'
task :package do
  puts DateTime.now.strftime('%Y-%m-%d-%H-%M-%S')
  system "tar pczf Renderer#{DateTime.now.strftime('%Y-%m-%d-%H-%M-%S')}.tar.gz *.dll"
end

desc 'Clean up the build'
task :clean do
  sh "rm Renderer.dll"
end
